
namespace Artworks.Infrastructure.Application.Query.CommonModel
{
    /// <summary>
    /// 筛选器运算符。
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 相等。
        /// </summary>
        Equal,
        /// <summary>
        /// 不相等。
        /// </summary>
        NotEqual,
        /// <summary>
        /// 大于。
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 小于。
        /// </summary>
        LessThan,
        /// <summary>
        /// 大于或等于。
        /// </summary>
        GreaterThanOrEquals,
        /// <summary>
        /// 小于或等于。
        /// </summary>
        LessThanOrEquals,
        /// <summary>
        /// 包含。
        /// </summary>
        Contains,
        /// <summary>
        /// 不包含。
        /// </summary>
        NotContains,
        /// <summary>
        /// 介于。
        /// </summary>
        Between,
        /// <summary>
        /// 不介于。
        /// </summary>
        NotBetween,
        /// <summary>
        /// 例如：'%key%'
        /// </summary>
        Like,
        /// <summary>
        ///  例如：'%key'
        /// </summary>
        LeftLike,
        /// <summary>
        ///   例如：'key%'
        /// </summary>
        RightLike,
        /// <summary>
        /// IN()
        /// </summary>
        In,
        /// <summary>
        /// NOT IN()
        /// </summary>
        NotIn,
    }
}
