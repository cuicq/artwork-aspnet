using System;
using System.Reflection;
using System.Reflection.Emit;
using Artworks.Container.Interceptors.InstanceInterceptors.InterfaceInterception;
using Artworks.Container;
using Artworks.Anomaly;
using Artworks.Container.CommonModel;
using Artworks.Container.CommonModel.Internal;

namespace Artworks.Container.Interceptors
{
    /// <summary>
    /// 对现有和新的对象进行拦截。
    /// </summary>
    public static class Intercept
    {

        /// <summary>
        /// 生成拦截实例
        /// </summary>
        /// <typeparam name="T">T is intercept type</typeparam>
        /// <param name="interceptor"></param>
        /// <returns></returns>
        public static T NewInstance<T>(Type interceptor) where T : class
        {

            if (interceptor == typeof(InterfaceInterceptor))
            {
                return Intercept.NewInstance<T>();
            }

            return default(T);
        }

        public static object NewInstance(Type interceptedType, Type interceptor)
        {
            try
            {

                var target = ServiceLocator.Instance.TryGetService(interceptedType);
                var proxyType = target.GetType();

                var instanceType = new InterfaceInterceptor().CreateProxy(interceptedType, proxyType);


                DynamicMethod method = new DynamicMethod("", interceptedType, null);
                var il = method.GetILGenerator();

                ConstructorInfo info = ((Type)instanceType).GetConstructor(Type.EmptyTypes);
                if (info == null)
                    return null;

                il.Emit(OpCodes.Newobj, info);
                il.Emit(OpCodes.Ret);

                var result = method.Invoke(info, null);

                return result;

            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 生成新拦截实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T NewInstance<T>() where T : class
        {

            try
            {
                Type interceptedType = typeof(T);

                var target = ServiceLocator.Instance.GetService<T>();
                var proxyType = target.GetType();

                var instanceType = new InterfaceInterceptor().CreateProxy(interceptedType, proxyType);

                var obj = CreateFunc<T>((Type)instanceType);
                var func = obj as Func<T>;

                if (func == null)
                    throw new System.Exception("unknown exception");
                return func();
            }
            catch (System.Exception ex)
            {
                ExceptionHelper.HandleException(ex);
            }
            return default(T);
        }

        #region Private methods

        /// <summary>
        /// 创建对象的构造方法委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Func<T> CreateFunc<T>(Type type)
        {

            DynamicMethod method = new DynamicMethod("", typeof(T), null);
            var il = method.GetILGenerator();

            ConstructorInfo info = type.GetConstructor(Type.EmptyTypes);
            if (info == null)
                return null;

            il.Emit(OpCodes.Newobj, info);
            il.Emit(OpCodes.Ret);


            return method.CreateDelegate(typeof(Func<T>)) as Func<T>;
        }


        #endregion
    }
}
