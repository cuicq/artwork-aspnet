using Artworks.Log.CommonModel.Internal;
using System;

namespace Artworks.Log.Persistence
{
    /// <summary>
    /// 文件存储。
    /// </summary>
    public class FileRepository : ILoggerRepository
    {
        private string file = string.Empty;

        public FileRepository(string name, string file, string storage = "none")
        {
            if (storage.ToLower() == "auto")
            {
                file = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, file);
            }
            this.file = file;
        }

        public void Execute(string message, Exception exception)
        {
            FileUtil.Write(this.file, exception == null ? message : message + " " + exception);
        }
    }
}
