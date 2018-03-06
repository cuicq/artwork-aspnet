using System;

namespace Artworks.Infrastructure.Application.Domain
{
    /// <summary>
    /// 领域实体接口。
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///唯一标识。
        /// </summary>
        Guid UniqueID { get; }
    }
}
