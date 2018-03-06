using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Configuration.Initialize;
using Artworks.Routing.CommonModel;
using Artworks.Routing.CommonModel.Internal;
using Artworks.Routing.Configuration.Interal;
using System.Web;

namespace Artworks.Routing.Internal
{
    /// <summary>
    /// 路由启动
    /// </summary>
    internal class RouteStartup
    {
        private static object lockObj = new object();
        private static bool started = false;

        /// <summary>
        /// 配置路由
        /// </summary>
        public static void Configure()
        {
            if (!started)
            {
                lock (lockObj)
                {
                    if (!started)
                    {
                        string path = HttpContext.Current.Server.MapPath("~/App_Data");

                        //默认注册配置容器
                        ConfigurationInitializerManager manager = new ConfigurationInitializerManager();
                        manager.Regist(new ConfigurationContext(new ConfigurationRegistry(ConfigurationKeyMap.APPLICATION, path)));
                        ConfigurationInitializerContainer.Instance.Initialize(manager);

                        manager.Regist(new ConfigurationContext(new ConfigurationRegistry(RoutingKeyMap.Routing, path)));

                        var routeRegistries = RouteRegistryConfiguration.Instance.GetValues<RouteRegistry>();
                        var routes = RouteDataDictionary.CreateIntance();
                        foreach (RouteRegistry registry in routeRegistries)
                        {
                            string location = registry.Location;
                            registry.Routes.ForEach(route =>
                            {
                                routes.AddRoute(location, route);
                            });
                        }
                        started = true;
                    }
                }
            }
        }
    }
}
