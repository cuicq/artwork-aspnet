using System;

namespace Artworks.Container.Interceptors.InterceptionBehaviors
{
    /// <summary>
    /// 异常拦截行为上下文。
    /// </summary>
    public class ExceptionInterceptionBehaviorContext : InterceptionBehaviorContext
    {

        /// <summary>
        /// 异常
        /// </summary>
        public System.Exception Exception { get; set; }

    }
}
