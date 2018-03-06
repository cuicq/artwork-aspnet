
namespace Artworks.Anomaly
{
    /// <summary>
    /// 异常信息处理接口。
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        bool Handle(System.Exception ex);
    }
}
