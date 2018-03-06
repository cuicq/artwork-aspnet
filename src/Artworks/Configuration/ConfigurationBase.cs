using Artworks.Anomaly;
using Artworks.Configuration.CommonModel;
using Artworks.Configuration.Initialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Configuration
{
    /// <summary>
    /// 配置信息基类。
    /// </summary>
    public abstract class ConfigurationBase : IConfiguration
    {
        /// <summary>
        /// 配置上下文
        /// </summary>
        public ConfigurationContext Context { get; private set; }

        //默认构造
        public ConfigurationBase()
            : this(Resource.APPLICATION_CONFIG)
        {

        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key">配置KEY</param>
        public ConfigurationBase(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, "config key");

            try
            {
                this.Context = ConfigurationInitializerContainer.Instance.GetConfigurationContext(key);

                Guard.ArgumentNotNull(this.Context, string.Format("Not found the {0} configuration files。", key));

                this.Execute(Context);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.HandleException(new ConfigurationException(ex.Message, ex));
                throw ex;
            }
        }

        /// <summary>
        /// 执行配置
        /// </summary>
        /// <param name="context"></param>
        protected abstract void Execute(ConfigurationContext context);

        /// <summary>
        /// 字典中获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object GetValue(string key)
        {
            return this.GetValue<object>(key);
        }

        /// <summary>
        /// 字典中获取指定类型配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public virtual T GetValue<T>(string key)
        {
            if (this.Context.Dictionary.ContainsKey(key))
                return (T)this.Context.Dictionary[key];
            return default(T);
        }

        /// <summary>
        /// 字典中获取指定类型所有配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IEnumerable<T> GetValues<T>()
        {
            List<T> list = new List<T>();
            foreach (var item in this.Context.Dictionary.Values)
            {
                if (item is T)
                {
                    list.Add((T)item);
                }
            }
            return list;
        }



    }
}
