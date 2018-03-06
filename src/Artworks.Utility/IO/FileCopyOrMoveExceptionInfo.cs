using System;

namespace Artworks.Utility.IO
{
    /// <summary>
    /// 文件拷贝过程的异常信息。
    /// </summary>
    public sealed class FileCopyOrMoveExceptionInfo
    {
        /// <summary>
        /// 源文件。
        /// </summary>
        public string SourceFile { get; internal set; }

        /// <summary>
        /// 目标文件。
        /// </summary>
        public string TargetFile { get; internal set; }

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
