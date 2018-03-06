
using Artworks.Container.CommonModel;
namespace Artworks.Container.Plugins
{
    /// <summary>
    /// 插件映射构建类。
    /// </summary>
    public class PluginMapBuilder
    {

        private readonly IPluginMap map;
        private readonly Registry[] registries;

        public PluginMapBuilder(Registry[] registries)
        {
            this.map = new PluginMap();
            this.registries = registries;
        }

        /// <summary>
        /// 构建映射
        /// </summary>
        /// <returns></returns>
        public IPluginMap Build()
        {

            foreach (var registry in this.registries)
            {
                foreach (var plugin in registry.Plugins)
                {
                    map.Add(plugin.PluginType, plugin);
                }
            }

            return map;
        }

    }

}
