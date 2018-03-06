using Artworks.Authorization.Configuration;
using Artworks.Container.Extensions.StructureMap;

namespace Artworks.Authorization
{
    /// <summary>
    /// 认证初始化
    /// </summary>
    public class AuthoricationStartup
    {
        public static void Configure()
        {
            //对象注入
            ContainerInitializer.Initialize(x =>
            {
                x.AddRegistry<PluginRegistry>();
            });
        }

    }
}
