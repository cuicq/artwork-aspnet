using System.Collections.Generic;

namespace Artworks.Cache.Core
{
    /// <summary>
    /// 缓存数据处理类。
    /// </summary>
    public class CacheData
    {
        public static Dictionary<TKey, TValue> GetCacheData<TKey, TValue>(string cacheKey)
        {
            return CacheFactory.Instance[cacheKey] as Dictionary<TKey, TValue>;
        }
        public static T GetCacheData<T>(string cacheKey) where T : class
        {
            return CacheFactory.Instance[cacheKey] as T;
        }
    }
}
