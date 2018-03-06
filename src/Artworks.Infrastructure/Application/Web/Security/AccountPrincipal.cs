using System.Security.Principal;

namespace Artworks.Infrastructure.Application.Web.Security
{

    /// <summary>
    /// 定义用户对象的基本功能。
    /// </summary>
    public class AccountPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string roleName)
        {
            if (Identity == null || !Identity.IsAuthenticated || string.IsNullOrEmpty(Identity.Name))
                return false;
            return false;
        }

        public AccountPrincipal(IIdentity identity)
        {
            this.Identity = identity;
        }
    }
}
