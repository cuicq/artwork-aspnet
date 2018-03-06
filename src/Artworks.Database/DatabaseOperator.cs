using Artworks.Database.CommonModel;
using Artworks.Database.CommonModel.Internal;
using Artworks.Database.Configuration;
using Artworks.Database.Initialize;
using Artworks.Database.Core.MySql;
using Artworks.Database.Core.SqlServer;
using System;

namespace Artworks.Database
{
    /// <summary>
    /// 表示该类为轻量级数据库框架
    /// </summary>
    public class DatabaseOperator
    {
        protected static object lockObj = new object();
        protected static volatile IDataProvider master;

        /// <summary>
        /// dbo所有者 
        /// master
        /// </summary>
        public static IDataProvider Master
        {
            get
            {
                if (master == null)
                {
                    lock (lockObj)
                    {
                        if (master == null)
                        {
                            string key = DatabaseConnectionStringKey.Master;
                            string connectionString = DatabaseInitializerContainer.Instance.GetConnectionString(key);
                            switch (DatabaseRegistryConfiguration.Instance.DataBaseType)
                            {
                                case DatabaseType.SqlServer:
                                    master = new SqlServerDataProvider(connectionString);
                                    break;
                                case DatabaseType.MySQL:
                                    master = new MySqlDataProvider(connectionString);
                                    break;
                            }
                        }
                    }
                }
                return master;
            }
        }
    }

}
