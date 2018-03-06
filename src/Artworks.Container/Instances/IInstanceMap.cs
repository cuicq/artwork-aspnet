using System;

namespace Artworks.Container.Instances
{
    /// <summary>
    /// 实例映射接口。
    /// </summary>
    public interface IInstanceMap
    {

        /// <summary>
        /// 添加实例
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="instance"></param>
        void Add(Type pluginType, Instance instance);

        /// <summary>
        /// 实例字典
        /// </summary>
        InstanceDictionary InstanceDictionary { get; }

    }
}
