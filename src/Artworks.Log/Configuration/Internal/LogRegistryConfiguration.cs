using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Log.CommonModel;
using System.Xml;

namespace Artworks.Log.Configuration.Internal
{
    /// <summary>
    /// 日志注册配置。
    /// </summary>
    internal class LogRegistryConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static string key = ArtworksConfiguration.Instance.GetValue<string>("log");//配置日志文件
        private static object lockObj = new object();
        private static LogRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static LogRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new LogRegistryConfiguration();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion

        
        public LogRegistryConfiguration()
            : base(key)
        {

        }

        protected override void Execute(ConfigurationContext context)
        {
            var document = context.Config.Build();
            var nodes = document.SelectNodes("/configuration/logger/section");
            foreach (XmlNode item in nodes)
            {
                string name = item.SelectSingleNode("name").InnerText.Trim().ToLower();

                LogRegistry registry = new LogRegistry();
                registry.Name = name;
                registry.Repository = item.SelectSingleNode("repository/type").InnerText.Trim();

                var paramNodes = item.SelectNodes("repository/construction/param");

                foreach (XmlNode param in paramNodes)
                {
                    string key = param.Attributes["name"].Value;
                    string value = param.Attributes["value"].Value;
                    if (!registry.Constructions.ContainsKey(key))
                    {
                        registry.Constructions.Add(key, value);
                    }
                }
                this.Context.Add(name, registry);
            }
        }
    }
}
