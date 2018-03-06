using System.Collections.Generic;

namespace Artworks.Routing.CommonModel
{
    /// <summary>
    /// 路由字典
    /// </summary>
    public class RouteDataDictionary
    {
        private static Dictionary<string, IList<RouteData>> routes = new Dictionary<string, IList<RouteData>>();

        private RouteDataDictionary()
        {

        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public static RouteDataDictionary CreateIntance()
        {
            return new RouteDataDictionary();
        }

        /// <summary>
        /// 添加路由定义
        /// </summary>
        public void AddRoute(string location, RouteData route)
        {
            if (!string.IsNullOrEmpty(location) && route != null)
            {
                if (routes.ContainsKey(location))
                {
                    routes[location].Add(route);
                }
                else
                {
                    IList<RouteData> list = new List<RouteData>();
                    list.Add(route);
                    routes.Add(location, list);
                }
            }
        }


        /// <summary>
        /// 是否路由
        /// </summary>
        public bool TryRoutes(string location, out IList<RouteData> list)
        {
            if (routes.ContainsKey(location))
            {
                list = routes[location];
            }
            else
            {
                list = new List<RouteData>();
            }
            return routes.ContainsKey(location);
        }

    }
}
