
using System.Collections.Generic;
namespace Artworks.Routing.CommonModel
{
    /// <summary>
    /// 路由注册。
    /// </summary>
    public class RouteRegistry
    {
        /// <summary>
        /// 定位快速查找
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 路由数据
        /// </summary>
        public List<RouteData> Routes { get; private set; }


        public RouteRegistry()
        {
            this.Routes = new List<RouteData>();
        }

    }
}
