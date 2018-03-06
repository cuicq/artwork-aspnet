using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Artworks.Routing.CommonModel.Internal
{
    /// <summary>
    /// 路由解析。
    /// </summary>
    internal class RouteParser
    {
        private const char separator = '/';

        public static string Parse(string path, string url)
        {
            string[] splitArray = SplitUrlToPathSegmentStrings(url);
            string location = string.Empty;
            if (splitArray.Length <= 1)
            {
                location = "default";
            }
            else
            {
                location = splitArray[0];
            }

            IList<RouteData> routes = null;
            RouteDataDictionary.CreateIntance().TryRoutes(location, out routes);

            string sendURL = string.Empty;

            foreach (RouteData route in routes)
            {
                var regex = new Regex("^" + RouteUtil.ResolveURL(path, route.Look) + "$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(url))
                {
                    sendURL = RouteUtil.ResolveURL(path, regex.Replace(url, route.Send));
                    break;
                }
            }

            return sendURL;
        }

        private static string[] SplitUrlToPathSegmentStrings(string url)
        {
            return url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
