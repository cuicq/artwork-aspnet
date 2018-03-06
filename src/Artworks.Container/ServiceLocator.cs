using System;
using System.Collections;
using ContainerExpression = Artworks.Container.CommonModel.Container;

namespace Artworks.Container
{
    /// <summary>
    /// Represents the Service Locator.
    /// </summary>
    public class ServiceLocator : ObjectContainer
    {
        private static readonly ServiceLocator instanceObj = new ServiceLocator();
        private readonly IObjectContainer container = ObjectContainerFactory.GetInstance();

        private ServiceLocator()
        {

        }

        /// <summary>
        /// Gets the singleton instance of the <c>ServiceLocator</c> class.
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return instanceObj; }
        }


        protected override object DoGetService(Type serviceType)
        {
            //只有获取单个实例时，才有拦截处理
            object target = null;
            TryInterceptor(serviceType, out target);
            if (target != null) return target;
            return this.container.GetService(serviceType);
        }

        protected override IList DoResolveAll(Type serviceType)
        {
            return this.container.ResolveAll(serviceType);
        }

        internal object TryGetService(Type serviceType)
        {
            return this.container.GetService(serviceType);
        }


        #region 拦截对象

        /// <summary>
        /// 试着获取
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="interceptor"></param>
        /// <returns></returns>
        private static bool TryInterceptor(Type pluginType, out object interceptor)
        {
            interceptor = null;
            var instances = ContainerExpression.Instances;
            if (instances != null)
            {
                if (instances.ContainsKey(pluginType))
                {
                    interceptor = instances[pluginType].Target;
                    return true;
                }
            }
            return false;
        }


        #endregion
    }
}
