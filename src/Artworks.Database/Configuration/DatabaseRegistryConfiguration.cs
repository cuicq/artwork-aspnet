using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Database.CommonModel;
using System;
using System.Xml;

namespace Artworks.Database.Configuration
{
    /// <summary>
    /// 数据库配置。
    /// </summary>
    public class DatabaseRegistryConfiguration : ConfigurationBase
    {

        #region 单例实例

        private static object lockObj = new object();
        private static DatabaseRegistryConfiguration instanceObj = null;

        /// <summary>
        /// 实例
        /// </summary>
        public static DatabaseRegistryConfiguration Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new DatabaseRegistryConfiguration();
                        }
                    }
                }
                return instanceObj;
            }
        }

        #endregion

        public DatabaseType DataBaseType { get; private set; }

        protected override void Execute(ConfigurationContext context)
        {
            var document = context.Config.Build();
            var nodes = document.SelectNodes(Resource.CONFIG_DATABASE_CONNECTION);

            DatabaseConnectionStringManager databaseManager = new DatabaseConnectionStringManager();

            foreach (XmlNode node in nodes)
            {
                var key = node.Attributes["key"].Value;
                var value = node.Attributes["value"].Value;

                if (!this.Context.Dictionary.ContainsKey(key))
                {
                    this.Context.Add(key, value);
                    //自动注册
                    databaseManager.Add(key, value);
                }
            }

            var typeNode = document.SelectSingleNode(Resource.CONFIG_DATABASE_TYPE);
            if (typeNode != null)
            {
                DatabaseType dbType = DatabaseType.SqlServer;
                Enum.TryParse<DatabaseType>(typeNode.InnerText.Trim(), out dbType);
                this.DataBaseType = dbType;
            }
            else
            {
                this.DataBaseType = DatabaseType.SqlServer;
            }
        }

    }
}
