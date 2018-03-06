using Artworks.Authorization.Provider;
using Artworks.Container.CommonModel;

namespace Artworks.Authorization.Configuration
{
    /// <summary>
    /// 插件对象注册。
    /// </summary>
    internal class PluginRegistry : Registry
    {
        public PluginRegistry()
        {
            For<IAuthorizationChain>().Use<AuthorizationProvider>();
        }

    }
}
