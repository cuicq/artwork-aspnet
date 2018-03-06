using System;

namespace Artworks.Container.Instances
{
    /// <summary>
    /// 对象实例类。
    /// </summary>
    public class Instance
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
        /// 目标对象实例
        /// </summary>
        public object Target { get; set; }
    }
}
