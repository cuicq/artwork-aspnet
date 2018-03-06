using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Artworks.Container.Interceptors.InterceptionBehaviors;

namespace Artworks.Container.Interceptors.TypeInterceptors.VirtualMethodInterception
{
    /// <summary>
    /// 
    /// </summary>
    public partial class InterceptingClassGenerator
    {

        private readonly Type typeToIntercept;
        private static readonly AssemblyBuilder assemblyBuilder;

        private TypeBuilder typeBuilder;
        private FieldBuilder targetField;

        static InterceptingClassGenerator()
        {
            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new System.Reflection.AssemblyName("Artworks.Dynamic.ClassesProxies"),
#if DEBUG_SAVE_GENERATED_ASSEMBLY
                 AssemblyBuilderAccess.RunAndSave
#else
 AssemblyBuilderAccess.Run
#endif
);
        }

        public InterceptingClassGenerator(Type typeToIntercept)
        {
            this.typeToIntercept = typeToIntercept;
            this.CreateTypeBuilder();
        }

        public Type CreateProxyType()
        {

            Type type = typeBuilder.CreateType();
#if DEBUG_SAVE_GENERATED_ASSEMBLY
            assemblyBuilder.Save("ArtworksClassesProxies.dll");
#endif
            return type;
        }

        #region Private methods

        private void CreateTypeBuilder()
        {
            TypeAttributes newAttributes = TypeAttributes.Public | TypeAttributes.Class;
            ModuleBuilder moduleBuilder = GetModuleBuilder();
            typeBuilder = moduleBuilder.DefineType(CreateTypeName(), newAttributes);
            this.BuildProxyField();
            this.BuildMethods();
        }

        private void BuildMethods()
        {
            MethodInfo[] methodInfos = typeToIntercept.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo methodInfo in methodInfos)
                OverrideMethod(methodInfo);
        }

        private void BuildProxyField()
        {
            targetField = typeBuilder.DefineField("target", typeToIntercept, FieldAttributes.Private);
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, null);
            ILGenerator generator = constructorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            ConstructorInfo defaultConstructorInfo = typeToIntercept.GetConstructor(Type.EmptyTypes);
            generator.Emit(OpCodes.Newobj, defaultConstructorInfo);
            generator.Emit(OpCodes.Stfld, targetField);
            generator.Emit(OpCodes.Ret);
        }

        private string CreateTypeName()
        {
            return "Artworks.DynamicModule.Wrapped_" + typeToIntercept.Name + "_" + Guid.NewGuid().ToString("N");
        }

        void OverrideMethod(MethodInfo method)
        {

            if (!method.IsPublic || !method.IsVirtual/*非虚方法无法重写*/|| method.IsFinal /*Final方法无法重写，如interface的实现方法标记为 virtual final*/ || IsObjectMethod(method)) return;

            const MethodAttributes methodattributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual;
            Type[] paramTypes = method.GetParameters().Select(ent => ent.ParameterType).ToArray();
            MethodBuilder mb = typeBuilder.DefineMethod(method.Name, methodattributes, method.ReturnType, paramTypes);
            ILGenerator il = mb.GetILGenerator();

            #region 初始化本地变量和返回值
            //加载所有参数到本地object[],实例方法第一个参数是this，已排除
            InterceptHelper.LoadArgumentIntoLocalField(il, paramTypes);

            //如果有返回值，定义返回值变量
            bool isReturnVoid = method.ReturnType == typeof(void);
            LocalBuilder result = null;
            if (!isReturnVoid)
                result = il.DeclareLocal(method.ReturnType);

            //定义MethodInfo变量，下面会用到（传递到Aop的上下文中）
            var methodInfo = il.DeclareLocal(typeof(MethodBase));
            il.Emit(OpCodes.Call, typeof(MethodBase).GetMethod("GetCurrentMethod", Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, methodInfo);
            #endregion

            #region 初始化InterceptionBehaviorContext
            Type contextType = typeof(InterceptionBehaviorContext);
            var context = il.DeclareLocal(contextType);
            ConstructorInfo info = contextType.GetConstructor(Type.EmptyTypes);
            il.Emit(OpCodes.Newobj, info);
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

            InterceptionBehaviorAttribute[] behaviors = InterceptionBehaviorHelper.GetInterceptionBehaviors(method);

            int behaviorLength = behaviors.Length;
            LocalBuilder[] lbs = new LocalBuilder[behaviorLength];
            MethodInfo[] endInvokeMethods = new MethodInfo[behaviorLength];
            MethodInfo[] invokingExceptionMethods = new MethodInfo[behaviorLength];

            #region 初始化标记的切面对象，并调用切面对象的beginInvoke方法
            for (int i = 0; i < behaviorLength; i++)
            {
                var tmpAttrType = behaviors[i].GetType();
                var tmpAttr = il.DeclareLocal(tmpAttrType);
                ConstructorInfo tmpAttrCtor = tmpAttrType.GetConstructor(Type.EmptyTypes);

                il.Emit(OpCodes.Newobj, tmpAttrCtor);
                il.Emit(OpCodes.Stloc, tmpAttr);

                var beforeInvokeMethod = tmpAttrType.GetMethod("BeginInvoke");
                endInvokeMethods[i] = tmpAttrType.GetMethod("EndInvoke");
                invokingExceptionMethods[i] = tmpAttrType.GetMethod("ExceptionInvoke");

                il.Emit(OpCodes.Ldloc, tmpAttr);
                il.Emit(OpCodes.Ldloc, context);
                il.Emit(OpCodes.Callvirt, beforeInvokeMethod);
                il.Emit(OpCodes.Nop);

                lbs[i] = tmpAttr;
            }
            #endregion

            //try
            il.BeginExceptionBlock();

            #region 调用实现方法
            if (!method.IsAbstract)
            {
                //类对象，参数值依次入栈
                for (int i = 0; i <= paramTypes.Length; i++)
                    il.Emit(OpCodes.Ldarg, i);

                //调用基类的方法
                il.Emit(OpCodes.Call, method);

                if (!isReturnVoid)
                {
                    il.Emit(OpCodes.Stloc, result);

                    //
                    il.Emit(OpCodes.Ldloc, context);
                    il.Emit(OpCodes.Ldloc, result);
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

            #region 调用切面对象的exceptionInvoke方法

            for (int i = 0; i < behaviorLength; i++)
            {
                il.Emit(OpCodes.Ldloc, lbs[i]);
                il.Emit(OpCodes.Ldloc, exceptionContent);
                il.Emit(OpCodes.Callvirt, invokingExceptionMethods[i]);
                il.Emit(OpCodes.Nop);
            }

            #endregion

            //try end
            il.EndExceptionBlock();

            #region 调用切面对象的endInvoke方法

            for (int i = 0; i < behaviorLength; i++)
            {
                il.Emit(OpCodes.Ldloc, lbs[i]);
                il.Emit(OpCodes.Ldloc, context);
                il.Emit(OpCodes.Callvirt, endInvokeMethods[i]);
                il.Emit(OpCodes.Nop);
            }
            #endregion

            if (!isReturnVoid)
                il.Emit(OpCodes.Ldloc, result);

            //返回
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
