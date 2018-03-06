using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Container.Plugins
{

    /// <summary>
    /// 插件字典类。
    /// </summary>
    public class PluginDictionary : Dictionary<Type, Plugin>
    {

        public PluginDictionary()
        {

        }

        public PluginDictionary(Type type, Plugin plugin)
        {
            this.Add(type, plugin);
        }

        public bool IsValid
        {
            get
            {
                return this.All(plugin => plugin.Value != null);
            }
        }


    }
}
