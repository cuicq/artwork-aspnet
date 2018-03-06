using System;
using System.Collections;
using StructureMapPlugin = StructureMap;

namespace Artworks.Container.Extensions.StructureMap.Internal
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    internal class StructureMapObjectContainer : ObjectContainer
    {

        protected override object DoGetService(Type serviceType)
        {
            return StructureMapPlugin.ObjectFactory.GetInstance(serviceType);
        }

        protected override IList DoResolveAll(Type serviceType)
        {
            var instances = StructureMapPlugin.ObjectFactory.GetAllInstances(serviceType);
            //  return instances.Cast<object>();

            return instances;
        }
    }
}
