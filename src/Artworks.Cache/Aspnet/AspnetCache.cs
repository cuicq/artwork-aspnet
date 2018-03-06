using System;
using System.Web;

namespace Artworks.Cache.Aspnet
{
    /// <summary>
    /// aspnet 缓存机制。
    /// </summary>
    public class AspnetCache : ICachePolicy
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        public override void AddObject(string key, object obj)
        {
            cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
        }

        public override void AddObject(string key, object obj, DateTime dateTime)
        {
            cache.Insert(key, obj, null, dateTime, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
        }

        public override void AddObject(string key, object obj, params string[] files)
        {
            cache.Insert(key, obj, new System.Web.Caching.CacheDependency(files), System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
        }

        public override void RemoveObject(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                cache.Remove(key);
            }
        }

        public override object RetrieveObject(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            return cache.Get(key);

        }
    }
}
