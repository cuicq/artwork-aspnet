using Artworks.Configuration.CommonModel;
using Artworks.Configuration.Initialize;

namespace Artworks.Configuration
{
    /// <summary>
    /// 配置初始化接口。
    /// </summary>
    public interface IConfigurationInitializer
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="manager"></param>
        void Initialize(ConfigurationInitializerManager manager);

        /// <summary>
        /// 根据配置KEY获取配置上下文
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ConfigurationContext GetConfigurationContext(string key);
    }
}
