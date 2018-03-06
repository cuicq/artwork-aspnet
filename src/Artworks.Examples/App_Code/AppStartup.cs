using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Configuration.Initialize;
using Artworks.Database.CommonModel;
using Artworks.Database.Configuration;
using Artworks.Database.Initialize;
using System;

namespace Artworks.Examples.App_Code
{
    /// <summary>
    /// 应用程序启动。
    /// </summary>
    public class AppStartup
    {
        public static void Configure()
        {
            //配置文件初始化
            string path = Environment.CurrentDirectory;
            ConfigurationInitializerManager manager = new ConfigurationInitializerManager();
            manager.Regist(new ConfigurationContext(new ConfigurationRegistry(ConfigurationKeyMap.APPLICATION, path)));
            ConfigurationInitializerContainer.Instance.Initialize(manager);

            //配置日志
            string logKey = ArtworksConfiguration.Instance.GetValue<string>("log");
            manager.Regist(new ConfigurationContext(new ConfigurationRegistry(logKey, path)));


            //数据库初始化
            string key = App_Code.DatabaseConnectionStringKey.Tester;
            string connectionString = DatabaseRegistryConfiguration.Instance.GetValue<string>(key);
            DatabaseConnectionStringManager databaseManager = new DatabaseConnectionStringManager();
            databaseManager.Add(key, connectionString);
            DatabaseInitializerContainer.Instance.SetInitializeDatabase(databaseManager);

        }

    }
}
