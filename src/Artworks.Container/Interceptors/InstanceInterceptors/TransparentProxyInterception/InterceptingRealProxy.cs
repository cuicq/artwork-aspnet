using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using Artworks.Container.Interceptors.InterceptionBehaviors;

namespace Artworks.Container.Interceptors.InstanceInterceptors.TransparentProxyInterception
{
    /// <summary>
    /// <see cref="代理拦截器"/>
    ///  可以拦截多个接口和按引用封送的对象上的公共实例方法。
    ///  这是最慢的拦截方式，但可以拦截的方法最多。
    ///  该拦截程序可以应用到新的和现有的实例。
    /// </summary>
    public class InterceptingRealProxy : RealProxy, IRemotingTypeInfo, IInterceptingProxy
    {

        private readonly object target;
        private string typeName;

        public InterceptingRealProxy(object target, Type classToProxy)
            : base(classToProxy)
        {

            Guard.ArgumentNotNull(target, "target");
            this.target = target;
            this.typeName = target.GetType().Name;
        }

        public object Target
        {
            get { return this.target; }
        }

        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
        {

            Guard.ArgumentNotNull(msg, "msg");
            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            var behaviors = InterceptionBehaviorHelper.GetInterceptionBehaviors(callMessage.MethodBase);

            var context = new InterceptionBehaviorContext
            {
                MethodInfo = callMessage.MethodBase,
                Arguments = callMessage.Args
            };

            //begin invoke
            this.beginInvoke(callMessage, behaviors, context);


            var ctor = callMessage as IConstructionCallMessage;

            if (ctor != null)
            {
                var defaultProxy = RemotingServices.GetRealProxy(this.target);
                defaultProxy.InitializeServerObject(ctor);
                var tp = (MarshalByRefObject)this.GetTransparentProxy();
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);
            }

            IMethodReturnMessage resultMsg = default(IMethodReturnMessage);
            var methodInfo = this.target.GetType().GetMethod(callMessage.MethodName);
            var newArray = callMessage.Args.ToArray();  //拷贝一份参数本地副本，用于从实际方法中接收out ,ref参数的值

            try
            {
                var resultValue = methodInfo.Invoke(this.target, newArray);
                context.ReturnValue = resultValue;
                resultMsg = new ReturnMessage(context.ReturnValue, newArray, newArray.Length, callMessage.LogicalCallContext, callMessage);
            }
            catch (System.Exception ex)
            {

                //exception invoke
                this.exceptionInvoke(behaviors, new ExceptionInterceptionBehaviorContext
                {
                    MethodInfo = context.MethodInfo,
                    Arguments = context.Arguments,
                    ReturnValue = context.ReturnValue,
                    Exception = ex.InnerException
                });

                var resultValue = methodInfo.ReturnType.IsValueType ? Activator.CreateInstance(methodInfo.ReturnType) : null;
                resultMsg = new ReturnMessage(resultValue, newArray, newArray.Length, callMessage.LogicalCallContext, callMessage);
            }

            //end invoke
            this.endInvoke(callMessage, resultMsg, behaviors, context);

            return resultMsg;
        }

        public bool CanCastTo(Type fromType, object o)
        {
            throw new NotImplementedException();
        }

        #region Privates methods

        private void beginInvoke(IMethodCallMessage requestMsg, IEnumerable<InterceptionBehaviorAttribute> interceptionBehaviors, InterceptionBehaviorContext context)
        {
            foreach (var behavior in interceptionBehaviors)
            {
                behavior.BeginInvoke(context);
            }
        }

        private void exceptionInvoke(IEnumerable<InterceptionBehaviorAttribute> interceptionBehaviors, ExceptionInterceptionBehaviorContext context)
        {

            foreach (var behavior in interceptionBehaviors)
            {
                behavior.ExceptionInvoke(context);
            }
        }

        private void endInvoke(IMethodCallMessage requestMsg, IMethodReturnMessage respond, IEnumerable<InterceptionBehaviorAttribute> interceptionBehaviors, InterceptionBehaviorContext context)
        {
            foreach (var behavior in interceptionBehaviors)
            {
                behavior.BeginInvoke(context);
            }
        }

        #endregion


    }
}
