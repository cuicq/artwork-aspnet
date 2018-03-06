using System;

namespace Artworks.Utility.IO
{
    /// <summary>
    /// 文件删除过程的异常信息。
    /// </summary>
    public sealed class FileDeleteExceptionInfo
    {
        /// <summary>
        /// 文件。
        /// </summary>
        public string File { get; internal set; }

        /// <summary>
        /// 错误信息。
        /// </summary>
        public Exception Exception { get; internal set; }

        /// <summary>
        /// 异常是否已经得到处理。
        /// </summary>
        public bool ExceptionHandled { get; set; }
    }
}
