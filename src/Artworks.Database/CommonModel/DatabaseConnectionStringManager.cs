using System.Collections;

namespace Artworks.Database.CommonModel
{
    /// <summary>
    /// 数据库链接字符串管理。
    /// </summary>
    public class DatabaseConnectionStringManager
    {

        private static Hashtable hashtable = Hashtable.Synchronized(new Hashtable());

        public DatabaseConnectionStringManager()
        {
            //do

        }


        public void Add(string key, string connectionString)
        {
            if (!hashtable.ContainsKey(key) && !string.IsNullOrEmpty(connectionString))
            {
                hashtable[key] = connectionString;
            }
        }

        public string GetConnectionString(string key)
        {
            var obj = hashtable[key];
            if (obj != null) return obj.ToString();

            var iteration = hashtable.GetEnumerator();
            if (iteration.MoveNext()) return iteration.Value.ToString();

            return string.Empty;
        }
    }
}
