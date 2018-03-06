using Artworks.Infrastructure.Application.Domain;
using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Query.CommonModel;
using System.Collections.Generic;

namespace Artworks.Infrastructure.Application.Persistence
{
    /// <summary>
    /// 仓储接口，这是一个标记接口。
    /// </summary>
    /// <typeparam name="T">聚合根类型。</typeparam>
    public interface IRepository<T> where T : class, IAggregateRoot
    {

        #region Properties

        /// <summary>
        /// 获取当前仓储所使用的仓储上下文实例。
        /// </summary>
        IRepositoryContext Context { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 将指定的聚合根添加到仓储中。
        /// </summary>
        /// <param name="model">需要添加到仓储的聚合根实例。</param>
        void Save(T model);

        /// <summary>
        /// 将指定的聚合根从仓储中移除。
        /// </summary>
        /// <param name="model">需要从仓储中移除的聚合根。</param>
        void Remove(T model);

        /// <summary>
        /// 更新指定的聚合根。
        /// </summary>
        /// <param name="model">需要更新的聚合根。</param>
        void Change(T model);

        /// <summary>
        /// 根据聚合根的ID值，从仓储中读取聚合根。
        /// </summary>
        /// <param name="key">聚合根的ID值。</param>
        /// <returns>聚合根实例。</returns>
        T FindBy(object id);

        /// <summary>
        /// 根据指定的规约，从仓储中获取唯一符合条件的聚合根。
        /// </summary>
        /// <param name="queryObject"></param>
        /// <returns></returns>
        T FindBy(QueryObject queryObject);

        /// <summary>
        /// 从仓储中读取所有聚合根。
        /// </summary>
        /// <returns>所有的聚合根。</returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// 根据指定的规约，从仓储中获取所有符合条件的聚合根。
        /// </summary>
        /// <param name="query">规约。</param>
        /// <returns>所有符合条件的聚合根。</returns>
        IEnumerable<T> FindAll(QueryObject queryObject);

        /// <summary>
        /// 根据指定的规约，以及分页参数，从仓储中读取所有聚合根。
        /// </summary>
        /// <param name="query">规约。</param>
        /// <param name="pageNumber">分页的页码。</param>
        /// <param name="pageSize">分页的页面大小。</param>
        /// <returns>带有分页信息的聚合根集合。</returns>
        PagedResult<T> FindAll(QueryObject queryObject, int pageNumber, int pageSize);


        #endregion

    }

}
