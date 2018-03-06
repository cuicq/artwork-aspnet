using Artworks.Database;
using Artworks.Database.Core.SqlServer;
using Artworks.Database.Initialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Database.Code
{
    public class DatabaseOperator
    {
        protected static object lockObj = new object();
        protected static volatile IDataProvider tester;

        /// <summary>
        /// dbo所有者 
        /// master
        /// </summary>
        public static IDataProvider Tester
        {
            get
            {
                if (tester == null)
                {
                    lock (lockObj)
                    {
                        if (tester == null)
                        {
                            string key = App_Code.DatabaseConnectionStringKey.Tester;
                            string connectionString = DatabaseInitializerContainer.Instance.GetConnectionString(key);
                            tester = new SqlServerDataProvider(connectionString);//获取连接字符串
                        }
                    }
                }
                return tester;
            }
        }
    }
}
