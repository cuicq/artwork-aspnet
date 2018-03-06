using Artworks.Cache.Core;
using Artworks.Examples.Codes.Cache.Code.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Artworks.Examples.Codes.Cache.Code
{
    public class UserData
    {
        private static object lockObj = new object();

        public static Dictionary<int, User> GetList1()
        {
            string cacheKey = CacheKey.CACHE_USER;
            string file = CacheFile.UserList;

            var data = CacheData.GetCacheData<int, User>(cacheKey);

            if (data == null && data.Count == 0)
            {
                data = new Dictionary<int, User>();
                var dt = CacheFileReader.ReadToDataTable(file);

                foreach (DataRow row in dt.Rows)
                {
                    User model = new User
                    {
                        ID = int.Parse(row["Id"].ToString()),
                        Name = row["name"].ToString(),
                        CreateDate = DateTime.Parse(row["CreateDate"].ToString())
                    };

                    data.Add(model.ID, model);
                }

                data = CacheBuild.Cache(cacheKey, file, data);
            }

            return data;
        }

        public static DataTable GetList2()
        {
            string cacheKey = CacheKey.CACHE_USER;
            string file = CacheFile.UserList;

            DataTable dt = CacheData.GetCacheData<DataTable>(cacheKey);
            if (dt == null)
            {
                lock (lockObj)
                {
                    dt = CacheFileReader.ReadToDataTable(file);
                    if (dt != null)
                    {
                        dt = CacheBuild.Cache(cacheKey, file, dt);
                    }
                }
            }
            return dt;
        }

        public static List<User> GetList3()
        {
            string cacheKey = CacheKey.CACHE_USER;
            string file = CacheFile.UserList;

            List<User> list = new List<User>();
            DataTable dt = CacheData.GetCacheData<DataTable>(cacheKey);
            if (dt == null)
            {
                lock (lockObj)
                {
                    dt = CacheFileReader.ReadToDataTable(file);
                    foreach (DataRow row in dt.Rows)
                    {
                        User model = new User
                        {
                            ID = int.Parse(row["Id"].ToString()),
                            Name = row["name"].ToString(),
                            CreateDate = DateTime.Parse(row["CreateDate"].ToString())
                        };
                        list.Add(model);
                    }
                    list = CacheBuild.Cache<User>(cacheKey, file);
                }
            }

            return list;
        }


    }
}
