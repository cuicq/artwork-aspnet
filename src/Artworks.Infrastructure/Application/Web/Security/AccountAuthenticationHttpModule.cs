using Artworks.Infrastructure.Application.Web.Security.Internal;
using System;
using System.Web;

namespace Artworks.Infrastructure.Application.Web.Security
{
    /// <summary>
    /// 账户认证处理。
    /// </summary>
    public class AccountAuthenticationHttpModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += context_AuthenticateRequest;
        }

        public void context_AuthenticateRequest(object sender, EventArgs e)
        {
            //得到当前的HttpContext
            var context = ((HttpApplication)sender).Context;
            HttpCookie cookie = context.Request.Cookies[ConfigUtil.CookieName];

            if (cookie != null)
            {
                var clientData = AccountAuthentication.RegisteredClientData;
                if (clientData != null)
                {
                    AccountIdentity identity = new AccountIdentity(clientData);
                    AccountPrincipal principal = new AccountPrincipal(identity);
                    context.User = principal;

                }
            }

        }
    }
}
