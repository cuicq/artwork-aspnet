using System;

namespace Artworks.Infrastructure.Application.Domain
{
    /// <summary>
    /// 聚合根类型的基类型。
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        protected Guid uniqueID;

        /// <summary>
        /// 获取当前领域实体类的全局唯一标识。
        /// </summary>
        public Guid UniqueID
        {
            get
            {
                if (this.uniqueID == Guid.Empty)
                    return Guid.NewGuid();
                return uniqueID;
            }
            set { uniqueID = value; }
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
            IAggregateRoot ar = obj as IAggregateRoot;
            if (ar == null)
                return false;
            return this.uniqueID == ar.UniqueID;
        }
        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>当前Object的哈希代码。</returns>
        /// <remarks>有关此函数的更多信息，请参见：http://msdn.microsoft.com/zh-cn/library/system.object.gethashcode。
        /// </remarks>
        public override int GetHashCode()
        {
            return this.uniqueID.GetHashCode();
        }


    }
}
