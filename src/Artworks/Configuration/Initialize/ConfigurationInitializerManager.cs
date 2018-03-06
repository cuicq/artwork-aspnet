using Artworks.Configuration.CommonModel;
using System.Collections;

namespace Artworks.Configuration.Initialize
{
    /// <summary>
    /// 配置初始化管理类。
    /// </summary>
    public sealed class ConfigurationInitializerManager
    {
        private static Hashtable container = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 注册配置上下文。
        /// </summary>
        /// <param name="context">配置上下文</param>
        public void Regist(ConfigurationContext context)
        {
            string key = context.Config.Name;
            if (!container.ContainsKey(key))
            {
                container.Add(key, context);
            }
        }

        /// <summary>
        /// 根据配置KEY获取配置上下文。
        /// </summary>
        /// <param name="key">配置KEY</param>
        /// <returns></returns>
        public ConfigurationContext GetConfigurationContext(string key)
        {
            ConfigurationContext context = null;
            if (container.ContainsKey(key))
            {
                context = (ConfigurationContext)container[key];
            }
            return context;
        }


    }
}
