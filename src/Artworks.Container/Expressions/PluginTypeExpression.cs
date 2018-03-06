using Artworks.Container.CommonModel;
using System;

namespace Artworks.Container.Expressions
{
    /// <summary>
    /// 具体类型扩展
    /// </summary>
    public class PluginTypeExpression
    {

        private Registry registry;
        private Type pluginType;

        public PluginTypeExpression(Type pluginType, Registry registry)
        {
            this.pluginType = pluginType;
            this.registry = registry;
        }

        public ConcreteTypeExpression Use<T>() where T : class
        {
            return this.Use(typeof(T));
        }

        public ConcreteTypeExpression Use(Type concreteType)
        {
            this.registry.Plugins.Add(this.pluginType, concreteType);
            ConcreteTypeExpression concreteTypeExpression = new ConcreteTypeExpression(this.pluginType, concreteType, this.registry);
            return concreteTypeExpression;
        }

        public Type PluginType
        {
            get { return this.pluginType; }
        }

    }
}
