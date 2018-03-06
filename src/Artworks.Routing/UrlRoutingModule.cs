using Artworks.Routing.CommonModel;
using Artworks.Routing.Internal;
using System;
using System.Web;

namespace Artworks.Routing
{
    /// <summary>
    /// 匹配定义的路由的 URL 请求。
    /// </summary>
    public abstract class UrlRoutingModule : IHttpModule
    {
        protected static RouteDataDictionary routes = RouteDataDictionary.CreateIntance();

        public UrlRoutingModule()
        {
            RouteStartup.Configure();
        }

        public virtual void Init(HttpApplication context)
        {
            context.AuthorizeRequest += AuthorizeRequest;
            context.PostReleaseRequestState += PostReleaseRequestState;
        }

        protected virtual void PostReleaseRequestState(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("ETag");
        }

        protected virtual void AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string url = app.Request.Path.ToLower();
            this.Execute(url, app, routes);
        }

        protected abstract void Execute(string url, HttpApplication app, RouteDataDictionary routes);

        /// <summary>
        /// 路由
        /// </summary>
        protected virtual void Route(HttpContext context, string routeURL)
        {
            string str = string.Empty;
            if (context.Request.QueryString.Count > 0)
            {
                if (routeURL.IndexOf('?') != -1)
                {
                    str = routeURL + "&" + context.Request.QueryString.ToString();
                }
                else
                {
                    str = routeURL + "?" + context.Request.QueryString.ToString();
                }
            }

            string url = routeURL;
            string queryString = string.Empty;
            int location = str.IndexOf('?');
            if (location > 0)
            {
                url = str.Substring(0, location);
                queryString = str.Substring(location + 1);
            }

            context.RewritePath(url, string.Empty, queryString);
        }

        public virtual void Dispose()
        {

        }
    }
}
