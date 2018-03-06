using System.Reflection;

namespace Artworks.Container.Interceptors.InterceptionBehaviors
{
    /// <summary>
    ///拦截行为上下文。
    /// </summary>
    public class InterceptionBehaviorContext
    {
        /// <summary>
        /// 
        /// </summary>
        public MethodBase MethodInfo { get; set; }

        public object ReturnValue { get; set; }

        public object[] Arguments { get; set; }


    }
}
