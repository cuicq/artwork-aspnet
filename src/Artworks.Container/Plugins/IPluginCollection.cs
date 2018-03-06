using System;

namespace Artworks.Container.Plugins
{
    /// <summary>
    /// 插件集合类型。
    /// </summary>
    public interface IPluginCollection
    {
        void Add(Type pluginType, Type concreteType);
        void Add(Type pluginType, Type concreteType, Type interceptType);
    }
}
