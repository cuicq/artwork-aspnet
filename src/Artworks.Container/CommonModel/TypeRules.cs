using System;

namespace Artworks.Container.CommonModel
{
    /// <summary>
    /// 实例类型规则。
    /// </summary>
    public class TypeRules
    {

        private Guid id;

        public TypeRules()
        {
            this.id = Guid.NewGuid();
        }

        /// <summary>
        /// 确定指定的Object是否等于当前的Object。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的Object与当前Object相等，则返回true，否则返回false。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.equals。
        /// </remarks>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            TypeRules ar = obj as TypeRules;
            if (ar == null)
                return false;
            return this.id == ar.ID;
        }
        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public Guid ID
        {
            get { return this.id; }
        }
    }
}
