using System;
using System.IO;

namespace Artworks.Utility.IO
{
    /// <summary>
    /// 文件工具。
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 如果不存在就创建一个文件，并设置默认内容，返回文件的路径。
        /// </summary>
        public static string CreateFile(string path, string defaultContent)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, defaultContent);
            }

            return path;
        }

        #region 移动文件

        /// <summary>
        /// 移动源目录中的满足搜索模型的文件到目标目录。
        /// </summary>
        public static void MoveFiles(
            string sourcePath,
            string searchPattern,
            string targetPath,
            Action<string, string> callback = null,
            Action<FileCopyOrMoveExceptionInfo> onError = null,
            bool allDirectories = true)
        {

            var files = Directory.EnumerateFiles(sourcePath, searchPattern, allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var sourceFile in files)
            {
                var targetFile = sourceFile.Replace(sourcePath, targetPath);

                MoveFiles(sourceFile, targetFile, callback, onError);
            }
        }

        private static void MoveFiles(
            string sourceFile,
            string targetFile,
            Action<string, string> callback,
            Action<FileCopyOrMoveExceptionInfo> onError)
        {
            try
            {
                File.Move(sourceFile, targetFile);

                if (callback != null)
                {
                    callback(sourceFile, targetFile);
                }
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    var exceptionInfo = new FileCopyOrMoveExceptionInfo
                    {
                        SourceFile = sourceFile,
                        TargetFile = targetFile,
                        Exception = ex
                    };
                    onError(exceptionInfo);

                    if (!exceptionInfo.ExceptionHandled)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region 拷贝文件

        /// <summary>
        /// 拷贝源目录中的满足搜索模型的文件到目标目录。
        /// </summary>
        public static void CopyFiles(
            string sourcePath,
            string searchPattern,
            string targetPath,
            Action<string, string> callback = null,
            Action<FileCopyOrMoveExceptionInfo> onError = null,
            bool allDirectories = true,
            bool overwrite = true)
        {

            var files = Directory.EnumerateFiles(sourcePath, searchPattern, allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var sourceFile in files)
            {
                var targetFile = sourceFile.Replace(sourcePath, targetPath);
                CopyFiles(sourceFile, targetFile, callback, onError, overwrite);
            }
        }

        private static void CopyFiles(
            string sourceFile,
            string targetFile,
            Action<string, string> callback,
            Action<FileCopyOrMoveExceptionInfo> onError,
            bool overwrite)
        {
            try
            {
                DirectoryUtil.CreateDirectoryIfNotExists(Path.GetDirectoryName(targetFile));

                File.Copy(sourceFile, targetFile, overwrite);

                if (callback != null)
                {
                    callback(sourceFile, targetFile);
                }
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    var exceptionInfo = new FileCopyOrMoveExceptionInfo
                    {
                        SourceFile = sourceFile,
                        TargetFile = targetFile,
                        Exception = ex
                    };
                    onError(exceptionInfo);

                    if (!exceptionInfo.ExceptionHandled)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 删除源目录中的满足搜索模型的文件。
        /// </summary>
        public static void DeleteFiles(
            string path,
            string searchPattern,
            Action<string> callback = null,
            Action<FileDeleteExceptionInfo> onError = null,
            bool allDirectories = true)
        {

            var files = Directory.EnumerateFiles(path, searchPattern, allDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                DeleteFiles(file, callback, onError);
            }
        }

        private static void DeleteFiles(
            string file,
            Action<string> callback,
            Action<FileDeleteExceptionInfo> onError)
        {
            try
            {
                File.Delete(file);

                if (callback != null)
                {
                    callback(file);
                }
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    var exceptionInfo = new FileDeleteExceptionInfo
                    {
                        File = file,
                        Exception = ex
                    };
                    onError(exceptionInfo);

                    if (!exceptionInfo.ExceptionHandled)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        #endregion
    }
}
