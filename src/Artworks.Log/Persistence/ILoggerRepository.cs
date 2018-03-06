using System;

namespace Artworks.Log.Persistence
{
    /// <summary>
    /// 日志仓储接口。
    /// </summary>
    public interface ILoggerRepository
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute(string message, Exception exception);
    }
}
