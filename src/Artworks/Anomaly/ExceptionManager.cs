using Artworks.Anomaly.CommonModel;
using Artworks.Anomaly.CommonModel.Internal;
using Artworks.Anomaly.Configuration.Internal;
using Artworks.Anomaly.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Anomaly
{
    /// <summary>
    /// 异常处理管理。
    /// </summary>
    public class ExceptionManager
    {
        #region Private Fields

        private static readonly ExceptionManager instanceObj = new ExceptionManager();
        private readonly Dictionary<Type, ExceptionHandingRegistry> handlersOrig = new Dictionary<Type, ExceptionHandingRegistry>();
        private readonly Dictionary<Type, List<IExceptionHandler>> handlersResponsibilityChain = new Dictionary<Type, List<IExceptionHandler>>();

        /// <summary>
        /// 实例
        /// </summary>
        private static ExceptionManager Instance { get { return instanceObj; } }
        private int RegisteredExceptionCountInternal { get { return handlersResponsibilityChain.Count; } }

        #endregion

        #region Ctor

        static ExceptionManager() { }

        private ExceptionManager()
        {
            try
            {
                var list = ExceptionRegistryConfiguration.Instance.GetValues<ExceptionRegistry>();
                foreach (ExceptionRegistry registry in list)
                {
                    Type exceptionType = Type.GetType(registry.Type);

                    if (exceptionType == null) continue;

                    if (exceptionType.IsAbstract ||
                        !typeof(System.Exception).IsAssignableFrom(exceptionType))
                        continue;

                    ExceptionHandlingBehavior behavior = registry.Behavior;

                    if (registry.Handlers != null && registry.Handlers.Count > 0)
                    {

                        foreach (ExceptionHandlerType handleItem in registry.Handlers)
                        {
                            Type handlerType = Type.GetType(handleItem.Type);
                            if (handlerType != null)
                            {
                                if (handlerType.IsAbstract ||
                                    !handlerType.GetInterfaces().Any(p => p.Equals(typeof(IExceptionHandler))))
                                    continue;

                                IExceptionHandler exceptionHandler = (IExceptionHandler)Activator.CreateInstance(handlerType);

                                this.RegisterHandlerOrig(exceptionType, behavior, exceptionHandler);

                            }


                        }
                    }
                    else
                    {
                        //如果未配置异常信息，则默认创建一个
                        this.handlersOrig.Add(exceptionType, new ExceptionHandingRegistry { Behavior = behavior });
                    }
                }

                this.BuildResponsibilityChain();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Private Methods

        private void BuildResponsibilityChain()
        {
            foreach (var kvp in handlersOrig)
            {
                List<IExceptionHandler> handlers = new List<IExceptionHandler>();
                kvp.Value.Handlers.ForEach(p => handlers.Add(p));

                switch (kvp.Value.Behavior)
                {
                    case ExceptionHandlingBehavior.Direct:
                        break;
                    case ExceptionHandlingBehavior.Forward:
                        List<IExceptionHandler> handlersFromBase = this.DumpBaseHandlers(kvp.Key);
                        handlersFromBase.ForEach(p => handlers.Add(p));
                        break;
                }

                handlersResponsibilityChain.Add(kvp.Key, handlers);
            }
        }

        private List<IExceptionHandler> DumpBaseHandlers(Type that)
        {
            List<IExceptionHandler> handlers = new List<IExceptionHandler>();
            Type baseType = that.BaseType;
            while (baseType != typeof(object))
            {
                if (handlersOrig.ContainsKey(baseType))
                {
                    var item = handlersOrig[baseType];
                    item.Handlers.ForEach(p => handlers.Add(p));
                }
                baseType = baseType.BaseType;
            }
            return handlers;
        }

        private void RegisterHandlerOrig(Type exceptionType, ExceptionHandlingBehavior behavior, IExceptionHandler handler)
        {
            if (handlersOrig.ContainsKey(exceptionType))
            {
                var ExceptionHandingRegistry = handlersOrig[exceptionType];
                var list = ExceptionHandingRegistry.Handlers;
                if (!list.Contains(handler, new ExceptionHandlerComparer()))
                {
                    list.Add(handler);
                }
            }
            else
            {
                ExceptionHandingRegistry registry = new ExceptionHandingRegistry();
                registry.Behavior = behavior;
                registry.Handlers.Add(handler);
                handlersOrig.Add(exceptionType, registry);
            }
        }

        private bool HandleExceptionInternal(System.Exception ex)
        {
            Type exceptionType = ex.GetType();
            Type curType = exceptionType;

            while (curType != null && curType.IsClass && typeof(System.Exception).IsAssignableFrom(curType))
            {
                if (handlersResponsibilityChain.ContainsKey(curType))
                {
                    var handlers = handlersResponsibilityChain[curType];
                    if (handlers != null && handlers.Count > 0)
                    {
                        bool ret = false;
                        handlers.ForEach(p => ret |= p.Handle(ex));
                        return ret;
                    }
                    else
                        return false;
                }
                curType = curType.BaseType;
            }

            return false;
        }
        private IEnumerable<IExceptionHandler> GetExceptionHandlersInternal(Type exceptionType)
        {

            Type curType = exceptionType;
            while (curType != null && curType.IsClass && typeof(System.Exception).IsAssignableFrom(curType))
            {
                if (handlersResponsibilityChain.ContainsKey(curType))
                    return handlersResponsibilityChain[curType];
                curType = curType.BaseType;
            }
            return new List<IExceptionHandler>();
        }

        private IEnumerable<IExceptionHandler> GetExceptionHandlersInternal<TException>() where TException : System.Exception
        {
            return GetExceptionHandlers(typeof(TException));
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// 获得一个特定的异常类型的异常处理程序列表
        /// </summary>
        public static IEnumerable<IExceptionHandler> GetExceptionHandlers(Type exceptionType)
        {
            return ExceptionManager.Instance.GetExceptionHandlersInternal(exceptionType);
        }

        /// <summary>
        /// 获得一个特定的异常类型的异常处理程序列表
        /// </summary>
        public static IEnumerable<IExceptionHandler> GetExceptionHandlers<TException>() where TException : System.Exception
        {
            return ExceptionManager.Instance.GetExceptionHandlersInternal<TException>();
        }

        /// <summary>
        /// 处理特定的异常
        /// </summary>
        public static bool HandleException(System.Exception ex)
        {
            return ExceptionManager.Instance.HandleExceptionInternal(ex);
        }

        /// <summary>
        /// 处理特定的异常
        /// </summary>
        public static bool HandleException<TException>(TException ex) where TException : System.Exception
        {
            return ExceptionManager.Instance.HandleExceptionInternal((System.Exception)ex);
        }

        #endregion
    }
}
