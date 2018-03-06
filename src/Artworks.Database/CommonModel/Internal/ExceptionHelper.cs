using Artworks.Anomaly;
using System;

namespace Artworks.Database.CommonModel.Internal
{
    /// <summary>
    /// 异常管理扩展。
    /// </summary>
    internal static class ExceptionHelper
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public static bool HandleException(Exception ex)
        {
            var innerException = new DatabaseException(ex.Message, ex);
            return ExceptionManager.HandleException(innerException);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        public static bool HandleException(string cmdText, Exception ex)
        {
            var innerException = new DatabaseException(cmdText, ex);
            return ExceptionManager.HandleException(innerException);
        }
    }
}
