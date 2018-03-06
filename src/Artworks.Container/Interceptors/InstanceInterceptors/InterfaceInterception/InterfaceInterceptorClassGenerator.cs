using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Artworks.Container.Interceptors.InterceptionBehaviors;
using System.Security;
using System.Diagnostics.CodeAnalysis;
using Artworks.Container;

namespace Artworks.Container.Interceptors.InstanceInterceptors.InterfaceInterception
{
    /// <summary>
    /// 生成拦截代理类。
    /// </summary>
    public partial class InterfaceInterceptorClassGenerator
    {

        private static readonly AssemblyBuilder assemblyBuilder;
        private readonly Type interceptedType;
        private readonly Type proxyType;
        private TypeBuilder typeBuilder;
        private FieldBuilder targetField;

        [SecurityCritical]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Need to use constructor so we can place attribute on it.")]
        static InterfaceInterceptorClassGenerator()
        {
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new System.Reflection.AssemblyName("Artworks.Dynamic")
                ,
#if DEBUG
 AssemblyBuilderAccess.RunAndSave
#else
                AssemblyBuilderAccess.Run
#endif
);
        }

        public InterfaceInterceptorClassGenerator(Type interceptedType, Type proxyType)
        {
            this.interceptedType = interceptedType;
            this.proxyType = proxyType;

            this.CreateTypeBuilder();
        }

        public Type CreateProxyType()
        {

            Type type = typeBuilder.CreateType();

#if DEBUG

            //assemblyBuilder.Save("Artworks.Dynamic.dll");

#endif

            return type;
        }

        #region Private methods

        private void CreateTypeBuilder()
        {
            ModuleBuilder moduleBuilder = GetModuleBuilder();
            typeBuilder = moduleBuilder.DefineType(CreateTypeName(), TypeAttributes.Public | TypeAttributes.Class, null, new[] { this.interceptedType });
            this.BuildProxyField();
            this.BuildMethods();
        }

        private void BuildMethods()
        {
            MethodInfo[] methodInfos = proxyType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo methodInfo in methodInfos)
                OverrideMethod(methodInfo);
        }

        //代理
        private void BuildProxyField()
        {

            MethodInfo method2 = typeof(ServiceLocator).GetMethod(
                "get_Instance",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[] { },
                null
                );
            MethodInfo method3 = typeof(Type).GetMethod(
                "GetTypeFromHandle",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(RuntimeTypeHandle) },
                null
                );


            MethodInfo method4 = typeof(ObjectContainer).GetMethod(
                "GetService",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(Type) },
                null
                );

            //    MethodInfo method5 = typeof(ObjectContainer).GetMethod("GetService").MakeGenericMethod(this.interceptedType);

            //http://msdn.microsoft.com/query/dev11.query?appId=Dev11IDEF1&l=ZH-CN&k=k(System.Reflection.MethodInfo.MakeGenericMethod);k(MakeGenericMethod);k(SolutionItemsProject);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5);k(DevLang-csharp)&rd=true


            targetField = typeBuilder.DefineField("_proxy", this.interceptedType, FieldAttributes.Private);
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, null);
            ILGenerator generator = constructorBuilder.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, method2);
            generator.Emit(OpCodes.Ldtoken, this.interceptedType);
            generator.Emit(OpCodes.Call, method3);
            generator.Emit(OpCodes.Callvirt, method4);
            generator.Emit(OpCodes.Castclass, this.interceptedType);

            generator.Emit(OpCodes.Stfld, targetField);
            generator.Emit(OpCodes.Ret);


        }

        //实例化不带构造函数的对象
        private void BuildProxyFieldEx()
        {
            targetField = typeBuilder.DefineField("_proxy", proxyType, FieldAttributes.Private);
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, null);
            ILGenerator generator = constructorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            ConstructorInfo defaultConstructorInfo = proxyType.GetConstructor(Type.EmptyTypes);
            generator.Emit(OpCodes.Newobj, defaultConstructorInfo);
            generator.Emit(OpCodes.Stfld, targetField);
            generator.Emit(OpCodes.Ret);
        }

        private string CreateTypeName()
        {

            //return "Artworks.DynamicModule.Wrapped_" + proxyType.Name + "_" + Guid.NewGuid().ToString("N");

            return proxyType.Name + "Proxy_" + Guid.NewGuid().ToString("N");
        }

        void OverrideMethod(MethodInfo method)
        {
            if (!method.IsPublic || IsObjectMethod(method))
                return;

            string methodName = method.Name;

            const MethodAttributes methodattributes = MethodAttributes.Public | MethodAttributes.Virtual;
            Type[] paramTypes = method.GetParameters().Select(ent => ent.ParameterType).ToArray();
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodName, methodattributes, method.ReturnType, paramTypes.ToArray());
            var il = methodBuilder.GetILGenerator();

            #region 初始化本地变量和返回值
            //加载所有参数到本地object[]
            InterceptHelper.LoadArgumentIntoLocalField(il, paramTypes);

            //如果有返回值，定义返回值变量
            bool isReturnVoid = method.ReturnType == typeof(void);
            LocalBuilder resultLocal = null;
            if (!isReturnVoid)
                resultLocal = il.DeclareLocal(method.ReturnType);

            //定义MethodInfo变量，下面会用到（传递到Aop的上下文中）
            var methodInfo = il.DeclareLocal(typeof(MethodBase));
            il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod("GetCurrentMethod", Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, methodInfo);
            #endregion


            #region 初始化InterceptionBehaviorContext

            Type contextType = typeof(InterceptionBehaviorContext);
            var context = il.DeclareLocal(contextType);
            il.Emit(OpCodes.Newobj, contextType.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, context);

            #endregion

            #region 给InterceptionBehaviorContext的参数值属性ParameterArgs,MethodInfo赋值
            il.Emit(OpCodes.Ldloc, context);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Call, contextType.GetMethod("set_Arguments"));

            il.Emit(OpCodes.Ldloc, context);
            il.Emit(OpCodes.Ldloc, methodInfo);
            il.Emit(OpCodes.Call, contextType.GetMethod("set_MethodInfo"));
            #endregion

            InterceptionBehaviorAttribute[] attrs = InterceptionBehaviorHelper.GetInterceptionBehaviors(method);

            int attrLen = attrs.Length;
            LocalBuilder[] lbs = new LocalBuilder[attrLen];
            MethodInfo[] endInvokeMethods = new MethodInfo[attrLen];
            MethodInfo[] invokingExceptionMethods = new MethodInfo[attrLen];

            #region 初始化标记的切面对象，并调用切面对象的BeginInvoke方法
            for (int i = 0; i < attrLen; i++)
            {
                var tmpAttrType = attrs[i].GetType();
                var tmpAttr = il.DeclareLocal(tmpAttrType);
                ConstructorInfo tmpAttrCtor = tmpAttrType.GetConstructor(Type.EmptyTypes);

                il.Emit(OpCodes.Newobj, tmpAttrCtor);
                il.Emit(OpCodes.Stloc, tmpAttr);

                var beginInvokeMethod = tmpAttrType.GetMethod("BeginInvoke");
                endInvokeMethods[i] = tmpAttrType.GetMethod("EndInvoke");
                invokingExceptionMethods[i] = tmpAttrType.GetMethod("ExceptionInvoke");

                il.Emit(OpCodes.Ldloc, tmpAttr);
                il.Emit(OpCodes.Ldloc, context);
                il.Emit(OpCodes.Callvirt, beginInvokeMethod);
                il.Emit(OpCodes.Nop);

                lbs[i] = tmpAttr;
            }
            #endregion

            il.BeginExceptionBlock();

            #region 调用实现方法
            if (!method.IsAbstract)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, targetField);
                for (int i = 0; i < paramTypes.Length; i++)
                    il.Emit(OpCodes.Ldarg, i + 1);  //arg_0为当前实例，故不添加到栈。

                il.Emit(OpCodes.Call, method);

                if (!isReturnVoid)
                {
                    il.Emit(OpCodes.Stloc, resultLocal);

                    //
                    il.Emit(OpCodes.Ldloc, context);
                    il.Emit(OpCodes.Ldloc, resultLocal);
                    if (method.ReturnType.IsValueType)
                        il.Emit(OpCodes.Box, method.ReturnType);
                    il.Emit(OpCodes.Call, contextType.GetMethod("set_ReturnValue"));
                }
            }
            #endregion

            //catch
            il.BeginCatchBlock(typeof(System.Exception));
            var exception = il.DeclareLocal(typeof(System.Exception));
            il.Emit(OpCodes.Stloc, exception);

            #region 初始化ExceptionInterceptionBehaviorContext
            var exceptionContentType = typeof(ExceptionInterceptionBehaviorContext);
            var exceptionContent = il.DeclareLocal(exceptionContentType);
            var exceptionContentCtor = exceptionContentType.GetConstructor(Type.EmptyTypes);
            il.Emit(OpCodes.Newobj, exceptionContentCtor);
            il.Emit(OpCodes.Stloc, exceptionContent);
            #endregion

            #region 给ExceptionInterceptionBehaviorContext的参数值属性ParameterArgs,MethodInfo,ExceptionInfo,赋值
            il.Emit(OpCodes.Ldloc, exceptionContent);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Call, exceptionContentType.GetMethod("set_Arguments"));

            il.Emit(OpCodes.Ldloc, exceptionContent);
            il.Emit(OpCodes.Ldloc, methodInfo);
            il.Emit(OpCodes.Call, exceptionContentType.GetMethod("set_MethodInfo"));

            il.Emit(OpCodes.Ldloc, exceptionContent);
            il.Emit(OpCodes.Ldloc, exception);
            il.Emit(OpCodes.Call, exceptionContentType.GetMethod("set_Exception"));
            #endregion

            #region 调用切面对象的ExceptionInvoke方法
            for (int i = 0; i < attrLen; i++)
            {
                il.Emit(OpCodes.Ldloc, lbs[i]);
                il.Emit(OpCodes.Ldloc, exceptionContent);
                il.Emit(OpCodes.Callvirt, invokingExceptionMethods[i]);
                il.Emit(OpCodes.Nop);
            }
            #endregion

            il.EndExceptionBlock();

            #region 调用切面对象的AfterInvoke方法
            for (int i = 0; i < attrLen; i++)
            {
                il.Emit(OpCodes.Ldloc, lbs[i]);
                il.Emit(OpCodes.Ldloc, context);
                il.Emit(OpCodes.Callvirt, endInvokeMethods[i]);
                il.Emit(OpCodes.Nop);
            }
            #endregion

            if (!isReturnVoid)
            {
                il.Emit(OpCodes.Ldloc, resultLocal);
            }

            il.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// 判断是否是基类Object的虚方法
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected bool IsObjectMethod(MethodInfo method)
        {
            string[] array = { "ToString", "GetType", "GetHashCode", "Equals" };
            return array.Contains(method.Name);
        }

        #endregion

    }
}
