

namespace Artworks.Container
{
    /// <summary>
    /// 对象容器工厂类。
    /// </summary>
    public class ObjectContainerFactory
    {

        private static IObjectContainer container;

        /// <summary>
        /// 初始化对象容器
        /// </summary>
        /// <param name="objectContainer"></param>
        public static void Initialize(IObjectContainer objectContainer)
        {
            container = objectContainer;
        }

        /// <summary>
        /// 获取对象容器实例
        /// </summary>
        /// <returns></returns>
        public static IObjectContainer GetInstance()
        {
            Guard.ArgumentNotNull(container, "container");
            return container;
        }

    }
}
