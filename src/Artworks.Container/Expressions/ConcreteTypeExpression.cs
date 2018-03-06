using Artworks.Container.CommonModel;
using System;
using System.Linq;

namespace Artworks.Container.Expressions
{
    /// <summary>
    /// 具体类型扩展。
    /// </summary>
    public class ConcreteTypeExpression
    {

        private Registry registry;
        private Type pluginType;
        private Type concreteType;
        private Type interceptType;

        public ConcreteTypeExpression(Type pluginType, Type concreteType, Registry registry)
        {
            this.pluginType = pluginType;
            this.concreteType = concreteType;
            this.registry = registry;
        }

        public void Interceptor<T>()
        {
            this.Interceptor(typeof(T));
        }

        public void Interceptor(Type interceptType)
        {
            this.interceptType = interceptType;
            var plugin = this.registry.Plugins.Where(x => x.PluginType == this.pluginType).FirstOrDefault();
            if (plugin == null) throw new ContainerException(Resource.EXCEPTION_NOEXISTS_PLUGIN);
            plugin.InterceptType = interceptType;
        }


    }
}
