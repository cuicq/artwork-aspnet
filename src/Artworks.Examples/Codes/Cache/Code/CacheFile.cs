using Artworks.Cache.Configuration.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Cache.Code
{
    /// <summary>
    /// 表示该类为缓存文件类
    /// </summary>
    public class CacheFile
    {
        public static string UserList = GetPath("user.xml");//用户数据缓存

        public static string GetPath(string fileName)
        {
            return Path.Combine(CacheRegistryConfiguration.Instance.CachePath, fileName);
        }

    }
}
