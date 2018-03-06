using System;
using System.Collections.Generic;
using StructureMapPlugin = StructureMap;
using ContainerExpression = Artworks.Container.CommonModel.Container;
using Artworks.Container.Plugins;
using Artworks.Container.CommonModel;
using Artworks.Anomaly;

namespace Artworks.Container.Extensions.StructureMap
{
    /// <summary>
    /// 表示该类为全局应用配置类
    /// </summary>
    internal class StructureMapConfiguration
    {

        public static void RegisterConfig()
        {
            StructureMapPlugin.ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ServiceRegistry>();
            });
        }

        private class ServiceRegistry : StructureMapPlugin.Configuration.DSL.Registry
        {
            public ServiceRegistry()
            {

                try
                {
                    foreach (KeyValuePair<Type, Plugin> item in ContainerExpression.Plugins)
                    {
                        var plugin = item.Value;
                        ForRequestedType(plugin.PluginType).TheDefaultIsConcreteType(plugin.ConcreteType);
                    }
                }
                catch (System.Exception ex)
                {
                    ExceptionManager.HandleException(new ContainerException(ex.Message, ex));
                    throw ex;
                }

            }
        }
    }

}
