using Artworks.Infrastructure.Application.Validation.CommonModel;

namespace Artworks.Infrastructure.Application.Validation
{
    /// <summary>
    /// 验证实体接口。
    /// </summary>
    /// <typeparam name="Model">实体</typeparam>
    public interface IValidator<Model>
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>验证状态结果</returns>
        ValidationState Validate(Model model);
    }
}
