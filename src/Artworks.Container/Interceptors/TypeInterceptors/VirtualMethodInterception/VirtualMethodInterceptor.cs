using System;

namespace Artworks.Container.Interceptors.TypeInterceptors.VirtualMethodInterception
{
    /// <summary>
    /// 
    /// </summary>
    public class VirtualMethodInterceptor : ITypeInterceptor
    {
        public Type CreateProxyType(Type type)
        {

            Guard.ArgumentNotNull(type, "type");

            Type interceptorType;

            InterceptingClassGenerator generator = new InterceptingClassGenerator(type);
            interceptorType = generator.CreateProxyType();

            return interceptorType;

        }
    }
}
