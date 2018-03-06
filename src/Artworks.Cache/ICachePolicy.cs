using System;

namespace Artworks.Cache
{
    /// <summary>
    /// 缓存策略抽象类。
    /// </summary>
    public abstract class ICachePolicy
    {
        protected ICachePolicy() { }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public abstract void AddObject(string key, object obj);
        public abstract void AddObject(string key, object obj, DateTime dateTime);
        public abstract void AddObject(string key, object obj, params string[] files);
        public abstract void RemoveObject(string key);
        public abstract object RetrieveObject(string key);

        public object this[string key]
        {
            get
            {
                return this.RetrieveObject(key);
            }
        }

    }
}
