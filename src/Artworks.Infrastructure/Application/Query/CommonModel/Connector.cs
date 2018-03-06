
namespace Artworks.Infrastructure.Application.Query.CommonModel
{
    /// <summary>
    /// 筛选器连接器。
    /// </summary>
    public enum Connector
    {
        /// <summary>
        /// 空。
        /// </summary>
        Empty,
        /// <summary>
        /// 非。
        /// </summary>
        Not,
        /// <summary>
        /// 与。
        /// </summary>
        And,
        /// <summary>
        /// 或。
        /// </summary>
        Or,
        /// <summary>
        /// 且非。
        /// </summary>
        AndNot,
        /// <summary>
        /// 或非。
        /// </summary>
        OrNot
    }
}
