using System;
using System.Collections.ObjectModel;

namespace Artworks.Container.Plugins
{
    /// <summary>
    /// 插件集合。
    /// </summary>
    public class PluginCollection : Collection<Plugin>, IPluginCollection
    {

        public void Add(Type pluginType, Type concreteType)
        {
            this.Add(new Plugin { PluginType = pluginType, ConcreteType = concreteType });
        }

        public void Add(Type pluginType, Type concreteType, Type interceptType)
        {
            this.Add(new Plugin { PluginType = pluginType, ConcreteType = concreteType, InterceptType = interceptType });
        }
    }
}
