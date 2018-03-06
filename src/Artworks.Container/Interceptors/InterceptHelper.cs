using System;
using System.Reflection.Emit;

namespace Artworks.Container.Interceptors
{
    /// <summary>
    /// 拦截对象帮助类。
    /// </summary>
    internal class InterceptHelper
    {
        /// <summary>
        /// 加载所有参数到本地object[],实例方法第一个参数是this，已排除
        /// </summary>
        /// <param name="il"></param>
        /// <param name="paramTypes"></param>
        public static void LoadArgumentIntoLocalField(ILGenerator il, Type[] paramTypes)
        {
            il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Ldc_I4, paramTypes.Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);
                if (paramTypes[i].IsValueType)
                    il.Emit(OpCodes.Box, paramTypes[i]);
                il.Emit(OpCodes.Stelem_Ref);
            }
        }

    }
}
