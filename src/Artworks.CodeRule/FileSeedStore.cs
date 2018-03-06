using Artworks.Configuration;
using Artworks.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /// <summary>
    /// 基于文件系统的种子仓储。
    /// </summary>
    public sealed class FileSeedStore : ISeedStore
    {
        private readonly string _seedFileDirectory;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public FileSeedStore()
        {
            var path = GetAppSettingsSeedFileDirectory();

            _seedFileDirectory = path;
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public FileSeedStore(string seedFileDirectory)
        {
            _seedFileDirectory = seedFileDirectory;
        }

        /// <inheritdoc />
        public int NextSeed(string seedKey)
        {
            var directory = DirectoryUtil.CreateDirectoryIfNotExists(_seedFileDirectory);

            string file = FileUtil.CreateFile(
                path: Path.Combine(directory, seedKey + ".txt"),
                defaultContent: (0 + ""));


            try
            {
                using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
                {
                    var reader = new StreamReader(fileStream);
                    var seed = int.Parse(reader.ReadToEnd()) + 1;

                    fileStream.Seek(0, SeekOrigin.Begin);
                    var writer = new StreamWriter(fileStream);
                    writer.Write(seed);
                    writer.Flush();

                    return seed;
                }
            }
            catch (IOException)
            {
                return this.NextSeed(seedKey);
            }
        }

        private static string GetAppSettingsSeedFileDirectory()
        {

            var path = ArtworksConfiguration.Instance.GetValue<string>("seeds");

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            return path;
        }
    }
}
