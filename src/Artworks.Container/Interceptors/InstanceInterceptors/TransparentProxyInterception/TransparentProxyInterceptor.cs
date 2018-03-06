using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Artworks.Container.Interceptors.InstanceInterceptors.TransparentProxyInterception {
    /// <summary>
    /// 表示该类为实例拦截器使用远程处理代理做拦截
    /// </summary>
    public class TransparentProxyInterceptor {

        public object CreateProxy(Type type, object target) {

            Guard.ArgumentNotNull(type, "type");
            Guard.ArgumentNotNull(target, "target");

            RealProxy realProxy = new InterceptingRealProxy(target, type);
            var proxyObj = realProxy.GetTransparentProxy();

            return proxyObj;
        }


    }
}
