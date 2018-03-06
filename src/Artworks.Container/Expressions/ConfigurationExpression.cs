using System;
using System.Collections.Generic;
using Artworks.Container.Plugins;
using Artworks.Container.CommonModel;

namespace Artworks.Container.Expressions
{
    /// <summary>
    /// 配置扩展。
    /// </summary>
    public class ConfigurationExpression : Registry
    {

        private readonly List<Registry> registries = new List<Registry>();

        public ConfigurationExpression()
        {
            this.registries.Add(this);
        }

        public void AddRegistry<T>() where T : Registry, new()
        {
            this.AddRegistry(Activator.CreateInstance<T>());
        }

        public void AddRegistry(Registry registry)
        {
            this.registries.Add(registry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IPluginMap BuildMap()
        {
            PluginMapBuilder builder = new PluginMapBuilder(this.registries.ToArray());
            return builder.Build();
        }

    }
}
