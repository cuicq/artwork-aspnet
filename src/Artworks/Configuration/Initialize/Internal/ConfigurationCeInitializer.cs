using Artworks.Configuration.CommonModel;

namespace Artworks.Configuration.Initialize.Internal
{
    /// <summary>
    /// 配置初始化接口实现类。
    /// </summary>
    internal class ConfigurationCeInitializer : IConfigurationInitializer
    {
        private ConfigurationInitializerManager manager;
        public void Initialize(ConfigurationInitializerManager manager)
        {
            if (this.manager == null)
            {
                this.manager = manager;
            }
        }

        public ConfigurationContext GetConfigurationContext(string key)
        {
            Guard.ArgumentNotNull(this.manager, "configuration initializer manager");
            Guard.ArgumentNotNullOrEmpty(key, "config key");

            return this.manager.GetConfigurationContext(key);
        }
    }
}
