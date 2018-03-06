using System;
using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Configuration;
using Artworks.Database.CommonModel;
using Artworks.Database.Initialize;
using Artworks.Infrastructure.Application.Configuration;
using Artworks.Configuration.Initialize;
using Artworks.Configuration.CommonModel;

namespace Artworks.Infrastructure.Application
{
    /// <summary>
    /// web应用程序。
    /// </summary>
    public abstract class WebApplication : IApplication, IDisposable
    {
        /// <summary>
        /// 应用程序启动事件。
        /// </summary>
        protected event EventHandler<ApplicationStartupEventArgs> OnStartup;

        /// <summary>
        /// 运行路径
        /// </summary>
        protected string Path { get; private set; }

        public WebApplication(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// 注册配置对象
        /// </summary>
        protected abstract void RegistConfigurationInitializerManager(ConfigurationInitializerManager manager);

        /// <summary>
        /// 注册数据库连接字符串 自定义
        /// </summary>
        protected abstract void RegistDatabaseConnectionStringManager(DatabaseConnectionStringManager manager);

        /// <summary>
        /// 配置
        /// </summary>
        protected virtual void Configure()
        {
            /*
             * 启动事件注册
             */
            EventHandler<ApplicationStartupEventArgs> startup = this.OnStartup;
            if (startup != null)
            {
                startup(this, new ApplicationStartupEventArgs(ApplicationConfiguration.Instance));
            }

            /*
             * 应用程序配置
             */

            ConfigurationInitializerManager manager = new ConfigurationInitializerManager();
            manager.Regist(new ConfigurationContext(new ConfigurationRegistry(ConfigurationKeyMap.APPLICATION, this.Path)));
            ConfigurationInitializerContainer.Instance.Initialize(manager);
            this.RegistConfigurationInitializerManager(manager);


            /*
             * 数据库初始化
             */
            DatabaseConnectionStringManager databaseManager = new DatabaseConnectionStringManager();
            this.RegistDatabaseConnectionStringManager(databaseManager);
            DatabaseInitializerContainer.Instance.SetInitializeDatabase(databaseManager);
        }


        /// <summary>
        /// 启动
        /// </summary>

        public virtual void Startup()
        {
            this.Configure();
        }

        public virtual void Dispose()
        {

        }
    }
}
