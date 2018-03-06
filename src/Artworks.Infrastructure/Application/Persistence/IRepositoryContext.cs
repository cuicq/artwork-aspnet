using Artworks.Infrastructure.Application.Domain;
using System;

namespace Artworks.Infrastructure.Application.Persistence
{
    /// <summary>
    /// 仓储上下文接口，这是一个标记接口。
    /// </summary>
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        #region Properties

        /// <summary>
        /// 获取仓储上下文的ID。
        /// </summary>
        Guid ID { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        /// <param name="unitOfWorkRepository">单元处理仓储实例</param>
        void RegisterNew<T>(T obj, IUnitOfWorkRepository unitOfWorkRepository) where T : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        /// <param name="unitOfWorkRepository">单元处理仓储实例</param>
        void RegisterModified<T>(T obj, IUnitOfWorkRepository unitOfWorkRepository) where T : class, IAggregateRoot;

        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        /// <param name="unitOfWorkRepository">单元处理仓储实例</param>
        void RegisterDeleted<T>(T obj, IUnitOfWorkRepository unitOfWorkRepository) where T : class, IAggregateRoot;

        #endregion
    }

}
