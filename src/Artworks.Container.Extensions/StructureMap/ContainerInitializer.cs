using System;
using Artworks.Container.Expressions;
using ContainerExpression = Artworks.Container.CommonModel.Container;
using Artworks.Container.Extensions.StructureMap.Internal;

namespace Artworks.Container.Extensions.StructureMap
{
    /// <summary>
    ///容器初始化类。
    /// </summary>
    public class ContainerInitializer
    {

        private static bool initialized = false;
        private static object lockObj = new object();

        public static void Initialize(Action<IInitializationExpression> action)
        {

            try
            {
                if (initialized == false)
                {
                    lock (lockObj)
                    {
                        if (initialized == false)
                        {
                            initialized = true;
                            ObjectContainerFactory.Initialize(new StructureMapObjectContainer());
                            ContainerExpression.Initialize(action);
                            StructureMapConfiguration.RegisterConfig();
                            ContainerExpression.Build();
                        }
                    }

                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
