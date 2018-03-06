using Artworks.Infrastructure.Application.Web.Security;

namespace Artworks.Infrastructure.Application.Web
{
    /// <summary>
    /// web处理接口。
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// 定义标识对象的基本功能
        /// </summary>
        IAccountIdentity Identity { get; }
    }
}
