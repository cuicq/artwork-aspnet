using System;
using System.Xml;
using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Container.CommonModel;

namespace Artworks.Container.Configuration
{
    /// <summary>
    /// 对象类型配置
    /// </summary>
    public class TypeRegistryConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static object lockObj = new object();
        private static TypeRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static TypeRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new TypeRegistryConfiguration();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion
        protected override void Execute(ConfigurationContext context)
        {
            var document = context.Config.Build();
            var list = document.SelectNodes(Resource.CONFIG_XPATH_CONTAINER);

            foreach (XmlNode item in list)
            {
                var type = item.Attributes["type"].Value;
                var mapTo = item.Attributes["mapTo"].Value;
                var intercept = item.Attributes["intercept"].Value;

                TypeRegistryMode mode = TypeRegistryMode.Dynamic;

                Enum.TryParse<TypeRegistryMode>(item.Attributes["mode"].Value.Trim(), out mode);

                var registry = new TypeRegistry(
                                   Type.GetType(type),
                                   Type.GetType(mapTo),
                                   Type.GetType(intercept),
                                   mode
                                   );

                this.Context.Add(type, registry);
            }
        }
    }
}
