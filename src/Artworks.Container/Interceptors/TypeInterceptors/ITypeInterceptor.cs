using System;

namespace Artworks.Container.Interceptors.TypeInterceptors {
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeInterceptor : IInterceptor {

        Type CreateProxyType(Type type);
    }
}
