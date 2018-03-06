using System;
using System.Xml;
using Artworks.Anomaly.CommonModel;
using Artworks.Configuration;
using Artworks.Configuration.CommonModel;

namespace Artworks.Anomaly.Configuration.Internal
{
    /// <summary>
    /// 异常处理注册配置
    /// </summary>
    internal class ExceptionRegistryConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static object lockObj = new object();
        private static ExceptionRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static ExceptionRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ExceptionRegistryConfiguration();
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
            var list = document.SelectNodes(Resource.CONFIG_XPATH_EXCEPTION);
            foreach (XmlNode parent in list)
            {
                string type = parent.Attributes["type"].Value;

                ExceptionHandlingBehavior behavior = ExceptionHandlingBehavior.Direct;
                Enum.TryParse<ExceptionHandlingBehavior>(parent.Attributes["behavior"].Value, out  behavior);

                ExceptionRegistry registry = new ExceptionRegistry { Type = type, Behavior = behavior };
                foreach (XmlNode node in parent.FirstChild.ChildNodes)
                {
                    var handler = node.Attributes["type"].Value;
                    ExceptionHandlerType target = new ExceptionHandlerType
                    {
                        Type = handler
                    };
                    registry.Handlers.Add(target);
                }
                if (registry.Handlers.Count > 0)
                {
                    this.Context.Add(type, registry);
                }
            }
        }
    }
}
