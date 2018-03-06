using Artworks.Cache.CommonModel;
using Artworks.Cache.Configuration.Internal;
using System;

namespace Artworks.Cache
{
    /// <summary>
    /// 缓存工厂。
    /// </summary>
    public class CacheFactory
    {
        private static ICachePolicy instanceObj = null;
        private static object lockObj = new object();

        /// <summary>
        /// 实例
        /// </summary>
        public static ICachePolicy Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            try
                            {
                                var cacheType = CacheRegistryConfiguration.Instance.CacheType;
                                instanceObj = (ICachePolicy)Activator.CreateInstance(Type.GetType(cacheType));
                            }
                            catch
                            {
                                throw new CacheException(Resource.EXCEPTION_CACHE_CONFIGURATION);
                            }
                        }
                    }
                }
                return instanceObj;
            }
        }

    }
}
