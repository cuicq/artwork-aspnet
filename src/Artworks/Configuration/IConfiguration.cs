using Artworks.Configuration.CommonModel;

namespace Artworks.Configuration
{
    /// <summary>
    /// 配置接口。
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// 配置上下文
        /// </summary>
        ConfigurationContext Context { get; }
    }
}
