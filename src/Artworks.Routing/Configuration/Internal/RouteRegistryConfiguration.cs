using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Routing.CommonModel;
using Artworks.Routing.CommonModel.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Artworks.Routing.Configuration.Interal
{
    /// <summary>
    /// 路由注册配置。
    /// </summary>
    internal class RouteRegistryConfiguration : ConfigurationBase
    {

        #region 单例实例

        private static object lockObj = new object();
        private static RouteRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static RouteRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new RouteRegistryConfiguration();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion

        public RouteRegistryConfiguration()
            : base(RoutingKeyMap.Routing)
        {

        }

        protected override void Execute(ConfigurationContext context)
        {
            var document = context.Config.Build();

            var list = document.SelectNodes("/routing/route");

            foreach (XmlNode item in list)
            {
                string location = string.Empty;
                if (item.Attributes.Count > 0)
                {
                    location = item.Attributes["location"].InnerText.Trim().ToLower();
                }
                else
                {
                    location = "default";
                }

                RouteRegistry registry = new RouteRegistry() { Location = location };

                foreach (XmlNode node in item.SelectNodes("rule"))
                {
                    RouteData route = new RouteData()
                    {
                        Look = node.SelectSingleNode("look").InnerText.Trim().ToLower(),
                        Send = node.SelectSingleNode("send").InnerText.Trim().ToLower()
                    };

                    registry.Routes.Add(route);
                }
                context.Add(location, registry);
            }
        }
    }
}
