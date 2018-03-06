using System;
using System.Collections;
using System.Collections.Generic;

namespace Artworks.Container
{
    /// <summary>
    /// 对象管理容器基类。
    /// </summary>
    public abstract class ObjectContainer : IObjectContainer
    {

        /// <summary>
        /// 获取特定的服务实例
        /// </summary>
        public virtual T GetService<T>() where T : class
        {
            return this.DoGetService(typeof(T)) as T;
        }

        /// <summary>
        /// 获取特定的服务实例
        /// </summary>
        public virtual object GetService(Type serviceType)
        {
            return this.DoGetService(serviceType);
        }

        /// <summary>
        /// 获取特定的服务实例集合
        /// </summary>
        public virtual IEnumerable<T> ResolveAll<T>() where T : class
        {
            //   return this.DoResolveAll(typeof(T)) as T[];
            var services = this.DoResolveAll(typeof(T));
            List<T> container = new List<T>();
            foreach (var service in services)
            {
                container.Add(service as T);
            }
            return container;
        }

        /// <summary>
        /// 获取特定的服务实例集合
        /// </summary>
        public IList ResolveAll(Type serviceType)
        {
            return this.DoResolveAll(serviceType);
        }

        /// <summary>
        /// 获取特定的服务实例集合
        /// </summary>
        protected abstract IList DoResolveAll(Type serviceType);

        /// <summary>
        /// 获取特定的服务实例
        /// </summary>
        protected abstract object DoGetService(Type serviceType);

    }
}
