using System;

namespace Artworks.Container.Plugins {
    /// <summary>
    /// 插件映射类。
    /// </summary>
    public class PluginMap : IPluginMap {

        private readonly PluginDictionary plugins = new PluginDictionary();

        public void Add(Type pluginType, Plugin plugin) {

            if (!this.plugins.ContainsKey(pluginType)) {
                this.plugins.Add(pluginType, plugin);
            }
        }


        public PluginDictionary PluginDictionary {
            get { return this.plugins; }
        }
    }
}
