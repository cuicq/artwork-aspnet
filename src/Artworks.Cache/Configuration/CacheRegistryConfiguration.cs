using Artworks.Configuration;
using Artworks.Configuration.CommonModel;

namespace Artworks.Cache.Configuration.Internal
{
    /// <summary>
    /// 缓存注册配置。
    /// </summary>
    public class CacheRegistryConfiguration : ConfigurationBase
    {

        #region 单例实例

        private static object lockObj = new object();
        private static CacheRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static CacheRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new CacheRegistryConfiguration();
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
            var node = document.SelectSingleNode(Resource.CONFIG_XPATH_CACHE);
            if (node != null)
            {
                this.CacheType = node.SelectSingleNode("type").InnerText.Trim();
                this.CachePath = node.SelectSingleNode("path").InnerText.Trim();
            }
        }

        /// <summary>
        /// 缓存路径
        /// </summary>
        public string CachePath { get; private set; }

        /// <summary>
        /// 缓存类型。
        /// </summary>
        public string CacheType { get; private set; }

    }
}
