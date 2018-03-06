using System;
using Artworks.Container.Expressions;
using Artworks.Container.Plugins;

namespace Artworks.Container.CommonModel
{

    /// <summary>
    /// 对象注册。
    /// </summary>
    public class Registry : IRegistry
    {

        private PluginCollection plugins = new PluginCollection();

        public Registry()
        {

        }

        public PluginTypeExpression For<T>() where T : class
        {
            var pluginType = typeof(T);
            return this.For(pluginType);
        }

        public PluginTypeExpression For(Type pluginType)
        {
            PluginTypeExpression concreteTypeExpression = new PluginTypeExpression(pluginType, this);
            return concreteTypeExpression;
        }

        public PluginCollection Plugins
        {
            get { return this.plugins; }
        }

    }
}
