using Artworks.Infrastructure.Application.Validation.CommonModel;

namespace Artworks.Infrastructure.Application.Validation
{
    /// <summary>
    /// 验证实体服务接口。
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// 验证实体
        /// </summary>
        /// <typeparam name="TModel">泛型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>验证状态结果</returns>
        ValidationState Validate<TModel>(TModel model);
    }
}
