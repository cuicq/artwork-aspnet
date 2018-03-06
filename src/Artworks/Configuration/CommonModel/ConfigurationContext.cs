using System.Collections.Generic;

namespace Artworks.Configuration.CommonModel
{
    /// <summary>
    /// 配置上下文。
    /// </summary>
    public class ConfigurationContext
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        public ConfigurationRegistry Config { get; private set; }
        /// <summary>
        /// 配置信息字典
        /// </summary>
        public Dictionary<string, object> Dictionary { get; private set; }

        public ConfigurationContext(ConfigurationRegistry config)
        {
            this.Config = config;
            this.Dictionary = new Dictionary<string, object>();
        }

        /// <summary>
        /// 配置字典添加配置信息
        /// 重复不添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Add(string key, object value)
        {
            if (!this.Dictionary.ContainsKey(key))
            {
                this.Dictionary.Add(key, value);
            }

        }
    }
}
