using Artworks.Routing.CommonModel;
using Artworks.Routing.CommonModel.Internal;

namespace Artworks.Routing
{
    /// <summary>
    /// 路由的 URL 请求。
    /// </summary>
    public sealed class UrlRouting : UrlRoutingModule
    {
        protected override void Execute(string url, System.Web.HttpApplication app, RouteDataDictionary routes)
        {
            if (!url.EndsWith("js") &&
                !url.EndsWith("css") &&
                !url.EndsWith("jpg") &&
                !url.EndsWith("gif") &&
                !url.EndsWith("png") &&
                !url.EndsWith("ashx") &&
                !url.EndsWith("aspx") &&
                !url.EndsWith("ico")
                )
            {
                string path = app.Context.Request.ApplicationPath;
                string routeURL = RouteParser.Parse(path, url);
                if (!string.IsNullOrEmpty(routeURL))
                {
                    base.Route(app.Context, routeURL);
                }
            }
        }
    }
}
