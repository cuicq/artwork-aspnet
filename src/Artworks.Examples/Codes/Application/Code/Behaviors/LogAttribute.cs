using Artworks.Container.Interceptors.InterceptionBehaviors;
using Artworks.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Application.Code.Behaviors
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogAttribute : InterceptionBehaviorAttribute
    {
        public override void BeginInvoke(InterceptionBehaviorContext context)
        {
            LogHelper.Debug(string.Format(" BeginInvoke  {0}", context.MethodInfo.Name));
        }

        public override void EndInvoke(InterceptionBehaviorContext context)
        {
            LogHelper.Debug(string.Format(" EndInvoke  {0}", context.MethodInfo.Name));
        }

        public override void ExceptionInvoke(ExceptionInterceptionBehaviorContext context)
        {
            LogHelper.Debug(string.Format(" ExceptionInvoke  {0}  Message : {1}", context.MethodInfo.Name, context.Exception.Message));
        }
    }
}
