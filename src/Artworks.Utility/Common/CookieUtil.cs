using System.Web;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// Cookie帮助类。
    /// </summary>
    public class CookieUtil
    {
        public static void SetCookie(string key, string value, int hours, string domain = "")
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Path = "/";
            if (!string.IsNullOrEmpty(domain)) cookie.Domain = domain;

            if (hours > 0)
                cookie.Expires = System.DateTime.Now.AddHours(hours);
            else
                cookie.HttpOnly = true;

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            string str = string.Empty;
            if (cookie != null) str = cookie.Value;
            return str;
        }

        public static void ClearCookie(string key, string domain = "")
        {
            HttpCookie cookie = new HttpCookie(key, "");
            cookie.Path = "/";
            cookie.Expires = System.DateTime.Now.AddHours(-100);
            if (!string.IsNullOrEmpty(domain)) cookie.Domain = domain;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ClearCookie()
        {
            System.Web.HttpContext.Current.Response.Cookies.Clear();
        }

    }
}
