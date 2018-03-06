using System;

namespace Artworks.Container.Interceptors.InterceptionBehaviors
{
    /// <summary>
    /// 拦截行为属性类。
    /// </summary>
    public abstract class InterceptionBehaviorAttribute : Attribute
    {
        /// <summary>
        /// 执行顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 是否阻塞，不触发
        /// </summary>
        public bool IsPrevent { get; set; }

        /// <summary>
        /// 是否自动在子类继承
        /// </summary>
        public bool IsAutoInherit { get; set; }

        /// <summary>
        /// 方法调用开始 拦截
        /// </summary>
        /// <param name="context"></param>
        public abstract void BeginInvoke(InterceptionBehaviorContext context);

        /// <summary>
        /// 方法调用结束 拦截
        /// </summary>
        /// <param name="context"></param>
        public abstract void EndInvoke(InterceptionBehaviorContext context);

        /// <summary>
        /// 程序异常 拦截
        /// </summary>
        /// <param name="context"></param>
        public abstract void ExceptionInvoke(ExceptionInterceptionBehaviorContext context);

    }
}
