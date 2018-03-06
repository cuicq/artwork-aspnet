using System;

namespace Artworks.Container.Plugins
{
    /// <summary>
    /// 插件映射接口类型。
    /// </summary>
    public interface IPluginMap
    {

        /// <summary>
        /// 添加插件
        /// </summary>
        void Add(Type pluginType, Plugin plugin);

        /// <summary>
        /// 插件字典
        /// </summary>
        PluginDictionary PluginDictionary { get; }
    }
}
