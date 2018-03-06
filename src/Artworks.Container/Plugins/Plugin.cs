using System;

namespace Artworks.Container.Plugins
{

    /// <summary>
    /// 插件类。
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// 插件类型
        /// </summary>
        public Type PluginType { get; set; }
        /// <summary>
        /// 具体实现类型
        /// </summary>
        public Type ConcreteType { get; set; }
        /// <summary>
        /// 拦截类型
        /// </summary>
        public Type InterceptType { get; set; }
    }
}
