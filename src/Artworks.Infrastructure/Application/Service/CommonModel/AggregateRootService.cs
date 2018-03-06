using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Domain;

namespace Artworks.Infrastructure.Application.Service.CommonModel
{
    /// <summary>
    /// 聚合根服务。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AggregateRootService<T> : IAggregateRootService<T> where T : class,IAggregateRoot
    {
        private ApplicationService<T> proxy;

        public AggregateRootService(ApplicationService<T> service)
        {
            this.proxy = service;
        }

        public ResponseResult Create(T model)
        {
            return this.proxy.Create(model);
        }

        public ResponseResult Update(T model)
        {
            return this.proxy.Update(model);
        }

        public ResponseResult Delete(RequestContext request)
        {
            return this.proxy.Delete(request);
        }
    }
}
