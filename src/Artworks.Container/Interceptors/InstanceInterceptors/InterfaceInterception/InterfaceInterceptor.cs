using System;

namespace Artworks.Container.Interceptors.InstanceInterceptors.InterfaceInterception {
    /// <summary>
    /// 实现拦截类型。
    /// </summary>
    public class InterfaceInterceptor : IInstanceInterceptor {

        public object CreateProxy(Type interceptedType, Type proxyType) {
            InterfaceInterceptorClassGenerator generator = new InterfaceInterceptorClassGenerator(interceptedType, proxyType);
            Type interceptorType = generator.CreateProxyType();
            return interceptorType;
        }
    }
}
