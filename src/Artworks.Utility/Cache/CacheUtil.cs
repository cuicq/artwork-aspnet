using System;
using System.Web;

namespace Artworks.Utility.Cache
{
    /// <summary>
    /// 设置ASP.NET页面缓存
    /// </summary>
    public class CacheUtil
    {
        /// <summary>
        /// 缓存页面头
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public static void ResponseCacheHeader(int hours, int minutes, int seconds)
        {
            TimeSpan span = new TimeSpan(hours, minutes, seconds);
            HttpContext.Current.Response.AddHeader("Cache-Control", "public,max-age=" + span.TotalSeconds.ToString());
        }

        #region 输出不缓存页头设置
        /// <summary>
        /// 输出不缓存页头设置
        /// </summary>
        /// <param name="context"></param>
        public static void ResponseNoCacheHead(System.Web.HttpContext context)
        {
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            context.Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);
        }
        #endregion
    }
}
