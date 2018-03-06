using Artworks.Configuration;
using Artworks.Configuration.CommonModel;
using Artworks.Configuration.Initialize;
using Artworks.Container.Extensions.StructureMap;
using Artworks.Database.CommonModel;
using Artworks.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Application.Code
{

    /// <summary>
    ///  web 应用程序
    /// </summary>
    public class WebApp : WebApplication
    {
        public WebApp()
            : base(Environment.CurrentDirectory)
        {

        }

        public override void Startup()
        {
            base.Startup();

            //对象注入
            ContainerInitializer.Initialize(x =>
            {
                x.AddRegistry<PluginRegistry>();
            });

        }

        /*
         * 应用配置文件
         */
        protected override void RegistConfigurationInitializerManager(ConfigurationInitializerManager manager)
        {
            manager.Regist(new ConfigurationContext(new ConfigurationRegistry(ConfigurationKeyMap.APPLICATION, this.Path)));

            //配置日志
            string logKey = ArtworksConfiguration.Instance.GetValue<string>("log");
            manager.Regist(new ConfigurationContext(new ConfigurationRegistry(logKey, this.Path)));

        }

        /*
         * 初始化数据库
         */
        protected override void RegistDatabaseConnectionStringManager(DatabaseConnectionStringManager manager)
        {
            //这里可以自定义
            //配置文件中的数据库已自动初始化
        }


    }
}
