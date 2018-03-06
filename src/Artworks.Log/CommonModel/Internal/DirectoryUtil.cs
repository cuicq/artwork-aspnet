using System.IO;
namespace Artworks.Log.CommonModel.Internal
{
    /// <summary>
    /// 文件目录工具类。
    /// </summary>
    public class DirectoryUtil
    {
        /// <summary>
        /// 如果不存在就创建一个目录，返回目录的路径。
        /// </summary>
        /// <returns></returns>
        public static string CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

    }
}
