
namespace Artworks.Container.CommonModel
{

    /// <summary>
    /// 容器泛型类型。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Container<T>
    {
        private T map;

        public Container(T map)
        {
            this.map = map;
        }

        /// <summary>
        /// 映射关系
        /// </summary>
        public T Map
        {
            get
            {
                return this.map;
            }
        }

    }
}
