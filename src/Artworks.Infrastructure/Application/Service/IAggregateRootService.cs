using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Domain;

namespace Artworks.Infrastructure.Application.Service
{
    /// <summary>
    /// 聚合根应用服务。
    /// </summary>
    public interface IAggregateRootService<T> where T : class ,IAggregateRoot
    {
        /// <summary>
        /// 创建聚合根。
        /// </summary>
        ResponseResult Create(T model);
        /// <summary>
        /// 修改聚合根。
        /// </summary>
        ResponseResult Update(T model);
        /// <summary>
        /// 删除聚合根。
        /// </summary>
        ResponseResult Delete(RequestContext request);
    }

}
