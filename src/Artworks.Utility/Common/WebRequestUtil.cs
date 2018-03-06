using System.Web;

namespace Artworks.Utility.Common
{
    /// <summary>
    /// web请求帮助类。
    /// </summary>
    public class WebRequestUtil
    {
        #region 参数处理

        /// <summary>
        /// 获得QueryString参数 String
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetQueryString(string parameter)
        {
            string val = HttpContext.Current.Request.QueryString[parameter];
            if (null == val) return string.Empty;
            return val;
        }

        /// <summary>
        /// 获得QueryString参数 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int GetQueryInt(string parameter, int defValue)
        {
            string val = HttpContext.Current.Request.QueryString[parameter];
            if (null == val) { return defValue; }
            int.TryParse(val, out defValue);
            return defValue;
        }


        /// <summary>
        /// 获得Form参数 String
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static string GetFormString(string parameter)
        {
            string val = HttpContext.Current.Request.Form[parameter];
            if (null == val) return string.Empty;
            return val;
        }

        /// <summary>
        /// 获得Form参数 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int GetFormInt(string parameter, int defValue)
        {
            string val = HttpContext.Current.Request.Form[parameter];
            if (null == val) { return defValue; }
            int.TryParse(val, out defValue);
            return defValue;
        }

        /// <summary>
        /// 获得参数，Query和Form通用 String
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static string GetString(string parameter)
        {
            string val = GetQueryString(parameter);
            if (string.IsNullOrEmpty(val))
                val = GetFormString(parameter);
            return val;
        }

        /// <summary>
        /// 获得参数，Query和Form通用 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string parameter, int defValue)
        {
            int val = GetQueryInt(parameter, defValue);
            if (val == defValue)
                val = GetFormInt(parameter, defValue);
            return val;
        }

        #endregion
    }
}
