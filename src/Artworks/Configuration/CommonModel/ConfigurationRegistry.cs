using System.Xml;

namespace Artworks.Configuration.CommonModel
{
    /// <summary>
    /// 配置注册。
    /// </summary>
    public class ConfigurationRegistry
    {
        private string name = string.Empty;
        private string path = string.Empty;

        public ConfigurationRegistry(string name, string path)
        {
            Guard.ArgumentNotNull(name, "file name");
            Guard.ArgumentNotNull(path, "file path");

            this.name = name;
            this.path = path;
        }

        /// <summary>
        /// 配置名
        /// </summary>
        public string Name { get { return this.name; } }

        /// <summary>
        /// 配置路径
        /// </summary>
        public string Path { get { return this.path; } }


        private XmlDocument document;

        /// <summary>
        /// 构建Xml对象
        /// </summary>
        /// <returns></returns>
        public XmlDocument Build()
        {
            if (this.document != null) return this.document;
            string file = System.IO.Path.Combine(this.path, this.name);
            if (System.IO.File.Exists(file))
            {
                this.document = new XmlDocument();
                this.document.Load(file);
            }
            else
            {
                throw new System.Exception(string.Format(Resource.EXCEPTION_NOTFOUND_APPFILE, file));
            }
            return this.document;
        }
    }
}
