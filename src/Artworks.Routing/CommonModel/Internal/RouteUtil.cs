
namespace Artworks.Routing.CommonModel.Internal
{
    /// <summary>
    /// 路由帮助。
    /// </summary>
    internal class RouteUtil
    {
        public static string ResolveURL(string path, string url)
        {
            if ((url.Length == 0) || (url[0] != '~'))
            {
                return url;
            }
            if (url.Length == 1)
            {
                return path;
            }
            if ((url[1] == '/') || (url[1] == '\\'))
            {
                if (path.Length > 1)
                {
                    return (path + "/" + url.Substring(2));
                }
                return ("/" + url.Substring(2));
            }
            if (path.Length > 1)
            {
                return (path + "/" + url.Substring(1));
            }
            return (path + url.Substring(1));
        }

    }
}
