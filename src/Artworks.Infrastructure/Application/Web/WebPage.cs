using Artworks.Infrastructure.Application.Web.Security;

namespace Artworks.Infrastructure.Application.Web
{
    /// <summary>
    /// web页面基类
    /// </summary>
    public class WebPage : System.Web.UI.Page, IHandler
    {
        /// <summary>
        /// SSO定义标识对象的基本功能
        /// </summary>
        public virtual IAccountIdentity Identity
        {
            get
            {
                if (!this.Context.User.Identity.IsAuthenticated)
                {
                    return new AccountIdentity();
                }
                return (IAccountIdentity)this.Context.User.Identity;
            }
        }
    }
}
