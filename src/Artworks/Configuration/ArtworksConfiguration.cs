using Artworks.Configuration.CommonModel;
using System.Xml;

namespace Artworks.Configuration
{
    /// <summary>
    /// Artworks框架配置。
    /// </summary>
    public class ArtworksConfiguration : ConfigurationBase
    {
        #region 单例实例

        private static object lockObj = new object();
        private static ArtworksConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static ArtworksConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ArtworksConfiguration();
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
            var list = document.SelectNodes("configuration/artworks/add");
            foreach (XmlNode item in list)
            {
                string key = item.Attributes["key"].Value;
                string value = item.Attributes["value"].Value;

                this.Context.Add(key, value);
            }
        }
    }
}
