
namespace Artworks.Infrastructure.Application.Web.Security
{
    /// <summary>
    ///  定义标识对象的基本功能。
    /// </summary>
    public interface IAccountIdentity : System.Security.Principal.IIdentity
    {
        int ID { get; }

        string Token { get; }

    }
}
