using System.Xml;
using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Schedule.CommonModel;

namespace Artworks.Schedule.Configuration.Internal
{
    /// <summary>
    /// 执行计划注册配置。
    /// </summary>
    internal class ScheduleRegistryConfiguration : ConfigurationBase
    {
        #region 实例

        private static object lockObj = new object();
        private static ScheduleRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例。
        /// </summary>
        public static ScheduleRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ScheduleRegistryConfiguration();
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
            var list = document.SelectNodes(Resource.CONFIG_XPATH_SCHEDULE);
            foreach (XmlNode item in list)
            {
                ScheduleRegistry registry = new ScheduleRegistry
                {
                    Name = item.Attributes["key"].Value,
                    Type = item.Attributes["value"].Value
                };
                this.Context.Add(registry.Name, registry);
            }
        }
    }

}
