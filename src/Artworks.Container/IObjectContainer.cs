using System;
using System.Collections;
using System.Collections.Generic;

namespace Artworks.Container
{
    /// <summary>
    /// 对象管理容器接口。
    /// </summary>
    public interface IObjectContainer : IServiceProvider
    {
        /// <summary>
        /// 获取特定的服务实例集合
        /// </summary>
        IEnumerable<T> ResolveAll<T>() where T : class;

        /// <summary>
        /// 获取特定的服务实例
        /// </summary>
        T GetService<T>() where T : class;

        /// <summary>
        ///  获取特定的服务实例类型集合
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        IList ResolveAll(Type serviceType);
    }
}
