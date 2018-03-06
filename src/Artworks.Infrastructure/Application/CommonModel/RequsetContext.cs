
using Artworks.Utility.Common;
using System.Web;
namespace Artworks.Infrastructure.Application.CommonModel
{

    /// <summary>
    /// 请求上下文
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestContext<T>
    {
        public HttpContext Context { get { return System.Web.HttpContext.Current; } }

        /// <summary>
        /// 请求数据
        /// </summary>
        public T Data { get; set; }

        #region 参数处理

        /// <summary>
        /// 获得QueryString参数 String
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string GetQueryString(string parameter)
        {
            return WebRequestUtil.GetQueryString(parameter);
        }

        /// <summary>
        /// 获得QueryString参数 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public int GetQueryInt(string parameter, int defValue)
        {
            return WebRequestUtil.GetQueryInt(parameter, defValue);
        }

        /// <summary>
        /// 获得Form参数 String
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public string GetFormString(string parameter)
        {
            return WebRequestUtil.GetFormString(parameter);
        }

        /// <summary>
        /// 获得Form参数 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public int GetFormInt(string parameter, int defValue)
        {
            return WebRequestUtil.GetFormInt(parameter, defValue);
        }

        /// <summary>
        /// 获得参数，Query和Form通用 String
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public string GetString(string parameter)
        {
            return WebRequestUtil.GetString(parameter);
        }

        /// <summary>
        /// 获得参数，Query和Form通用 Int
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public int GetInt(string parameter, int defValue)
        {
            return WebRequestUtil.GetInt(parameter, defValue);
        }

        #endregion
    }

    /// <summary>
    /// 请求上下文。
    /// </summary>
    public class RequestContext
    {
        public HttpContext Context { get { return System.Web.HttpContext.Current; } }

        /// <summary>
        /// 请求数据
        /// </summary>
        public object Data { get; set; }
    }
}
