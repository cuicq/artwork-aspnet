using Artworks.Infrastructure.Application.CommonModel;
using Artworks.Infrastructure.Application.Web.Security;
using Artworks.Utility.Common;
using System.Web;

namespace Artworks.Infrastructure.Application.Web
{

    /// <summary>
    /// 表示该类为web一般处理程序类
    /// </summary>
    public abstract class WebHandler : IHttpHandler, IHandler
    {
        public HttpContext Context { get; private set; }

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

        public virtual void ProcessRequest(HttpContext context)
        {
            this.Context = context;
            this.ProcessRequest();
        }

        /// <summary>
        /// 请求处理
        /// </summary>
        protected abstract void ProcessRequest();


        /// <summary>
        /// 标准输出到客户端
        /// </summary>
        protected virtual void Output(ResponseResult result)
        {
            this.Context.Response.Write(result.ToString());
            this.Context.Response.Flush();
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
