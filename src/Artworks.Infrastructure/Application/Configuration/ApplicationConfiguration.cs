using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using System.Xml;

namespace Artworks.Infrastructure.Application.Configuration
{
    /// <summary>
    /// 应用程序配置。
    /// </summary>
    public class ApplicationConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static object lockObj = new object();
        private static ApplicationConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static ApplicationConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ApplicationConfiguration();
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
            var list = document.SelectNodes(Resource.CONFIG_XPATH_APPLICATION);
            foreach (XmlNode item in list)
            {
                string key = item.Attributes["key"].Value;
                string value = item.Attributes["value"].Value;

                this.Context.Add(key, value);
            }
        }
    }

}
