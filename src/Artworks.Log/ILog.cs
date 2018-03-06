
namespace Artworks.Log
{
    /// <summary>
    /// 日志接口。
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        void Log(string message, System.Exception exception);
    }
}
