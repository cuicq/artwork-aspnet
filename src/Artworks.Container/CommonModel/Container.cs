using System;
using System.Collections.Generic;
using Artworks.Container.Expressions;
using Artworks.Container.Instances;
using Artworks.Container.Plugins;
using Artworks.Container.Interceptors;

namespace Artworks.Container.CommonModel
{
    /// <summary>
    /// 对象存储容器类。
    /// </summary>
    public class Container
    {

        #region Private properties

        private static readonly object lockObj = new object();
        private static Container<IPluginMap> pluginContainerObj;
        private static Container<IInstanceMap> instanceContainerObj;

        #endregion

        #region Public methods

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        public static void Initialize(Action<IInitializationExpression> action)
        {

            lock (typeof(Container))
            {
                InitializationExpression expression = new InitializationExpression();
                action(expression);

                IPluginMap map = expression.BuildMap();
                pluginContainerObj = new Container<IPluginMap>(map);

            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Build()
        {
            //初始化拦截器
            Container.Initialize(x =>
            {
                foreach (KeyValuePair<Type, Plugin> item in Plugins)
                {
                    var plugin = item.Value;
                    if (plugin.InterceptType != null)
                    {
                        var target = Intercept.NewInstance(plugin.PluginType, plugin.InterceptType);
                        x.Add(plugin.PluginType, plugin.ConcreteType, target);
                    }
                }

            });
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        private static void Initialize(Action<IInstanceExpression> action)
        {

            lock (typeof(Container))
            {

                InstanceExpression expression = new InstanceExpression();
                action(expression);

                IInstanceMap map = expression.BuildMap();
                instanceContainerObj = new Container<IInstanceMap>(map);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// 实例集合
        /// </summary>
        internal static InstanceDictionary Instances
        {
            get
            {
                if (instanceContainer.Map == null) return null;

                return instanceContainer.Map.InstanceDictionary;
            }
        }

        /// <summary>
        /// 插件集合
        /// </summary>
        public static PluginDictionary Plugins
        {
            get
            {
                if (pluginContainer.Map == null) return null;
                return pluginContainer.Map.PluginDictionary;
            }
        }

        #endregion

        #region Private properties

        private static Container<IPluginMap> pluginContainer
        {
            get
            {
                if (pluginContainerObj == null)
                {
                    lock (lockObj)
                    {
                        if (pluginContainerObj == null)
                        {
                            pluginContainerObj = new Container<IPluginMap>(null);
                        }
                    }
                }
                return pluginContainerObj;
            }
        }

        private static Container<IInstanceMap> instanceContainer
        {
            get
            {

                if (instanceContainerObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceContainerObj == null)
                        {
                            instanceContainerObj = new Container<IInstanceMap>(null);
                        }
                    }
                }
                return instanceContainerObj;
            }
        }


        #endregion

    }



}
