using System;

namespace Artworks.Container.Interceptors.InstanceInterceptors
{
    /// <summary>
    /// 实例拦截信息接口。
    /// </summary>
    public interface IInstanceInterceptor : IInterceptor
    {

        /// <summary>
        /// 创建代理拦截对象
        /// </summary>
        /// <param name="interceptedType">拦截类型</param>
        /// <param name="typeToProxy">代理类型</param>
        /// <returns></returns>
        object CreateProxy(Type interceptedType, Type proxyType);

    }
}
