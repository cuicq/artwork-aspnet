
namespace Artworks.Infrastructure.Application.Domain
{
    /// <summary>
    /// 数据仓储单元处理接口。
    /// </summary>
    public interface IUnitOfWorkRepository
    {
        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        void PersistCreationOf<T>(T model);
        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        void PersistUpdateOf<T>(T model);
        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        void PersistDeletionOf<T>(T model);
    }
}
