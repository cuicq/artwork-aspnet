using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Artworks.Cache.Core
{
    /// <summary>
    /// 表示该类为缓存构建类
    /// </summary>
    public class CacheBuild
    {
        /// <summary>
        /// 构建缓存
        /// </summary>
        /// <typeparam name="T">要转的类型</typeparam>
        /// <param name="cachekey">缓存键</param>
        /// <param name="file">文件路径</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static T Cache<T>(string cachekey, string file, T obj) where T : class
        {
            if (CacheFactory.Instance[cachekey] == null)
            {
                CacheFactory.Instance.AddObject(cachekey, obj, file);
            }
            return CacheFactory.Instance[cachekey] as T;
        }

        public static DataTable Cache(string cachekey, string file)
        {
            if (CacheFactory.Instance[cachekey] == null)
            {
                DataTable dt = new DataTable();
                try
                {
                    dt.ReadXml(file);
                }
                catch
                {
                    System.Threading.Thread.Sleep(5);
                    dt.ReadXml(file);
                }
                CacheFactory.Instance.AddObject(cachekey, dt, file);
            }

            return CacheFactory.Instance[cachekey] as DataTable;
        }

        public static Dictionary<TKey, TValue> Cache<TKey, TValue>(string cachekey, string file)
        {
            if (CacheFactory.Instance[cachekey] == null)
            {
                DataTable dt = new DataTable();
                try
                {
                    dt.ReadXml(file);
                }
                catch
                {
                    System.Threading.Thread.Sleep(5);
                    dt.ReadXml(file);
                }
                Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
                foreach (DataRow row in dt.Rows)
                {
                    TKey objKey = (TKey)(row["key"]);
                    if (!dictionary.ContainsKey(objKey))
                    {
                        dictionary.Add((TKey)(row["key"]), (TValue)(row["value"]));
                    }
                }
                CacheFactory.Instance.AddObject(cachekey, dictionary, file);
            }

            return CacheFactory.Instance[cachekey] as Dictionary<TKey, TValue>;
        }

        public static List<T> Cache<T>(string cachekey, string file)
        {
            if (CacheFactory.Instance[cachekey] == null)
            {
                List<T> list = Deserialize<T>(file);
                CacheFactory.Instance.AddObject(cachekey, list, file);
            }
            return CacheFactory.Instance[cachekey] as List<T>;
        }


        #region 二进制反序列化

        /// <summary>
        /// 二进制数据反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        private static List<T> Deserialize<T>(string file)
        {
            List<T> list = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                list = bf.Deserialize(fs) as List<T>;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return list;
        }

        #endregion


    }
}
