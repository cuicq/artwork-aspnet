using Artworks.Database.CommonModel;
using Artworks.Database.Configuration;
using Artworks.Database.Core.MySql;
using Artworks.Database.Core.SqlServer;
using System;

namespace Artworks.Database.Core
{
    /// <summary>
    /// 数据库参数操作类  
    /// 在这里可以实现多个不同数据库例如：SqlServer、MySql 、Accessd配置
    /// </summary>
    public class DataParameterProvider
    {
        #region 变量

        /// <summary>
        /// 线程安全
        /// </summary>
        private static object lockObj = new object();

        /// <summary>
        /// 参数操作类
        /// </summary>
        private static DataParameter instanceObj = null;

        #endregion

        #region 参数实例
        /// <summary>
        /// 参数实例
        /// </summary>
        public static DataParameter Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            switch (DatabaseRegistryConfiguration.Instance.DataBaseType)
                            {
                                case DatabaseType.SqlServer:
                                    instanceObj = new SqlServerParameter();
                                    break;
                                case DatabaseType.MySQL:
                                    instanceObj = new MySqlParameter();
                                    break;
                            }
                        }
                    }
                }
                return instanceObj;
            }
        }
        #endregion
    }

}
