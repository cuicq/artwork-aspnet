using Artworks.Infrastructure.Application.Domain;
using Artworks.Infrastructure.Application.Query;
using Artworks.Infrastructure.Application.Query.CommonModel;
using System;
using System.Collections.Generic;

namespace Artworks.Infrastructure.Application.Service
{
    /// <summary>
    /// 应用程序服务接口。
    /// </summary>
    public interface IApplicationService<T> : IDisposable where T : class ,IAggregateRoot
    {
        /// <summary>
        /// 聚合根服务实例
        /// </summary>
        IAggregateRootService<T> AggregateRootService { get; }

        T Get(object id);

        IEnumerable<T> GetList();
        PagedResult<T> GetList(QueryObject query, int pageIndex, int pageSize);
    }
}
