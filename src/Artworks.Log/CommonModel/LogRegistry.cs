using System.Collections.Generic;

namespace Artworks.Log.CommonModel
{
    /// <summary>
    /// 日志注册。
    /// </summary>
    public class LogRegistry
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 仓储
        /// </summary>
        public string Repository { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        public Dictionary<string, string> Constructions { get; set; }

        public LogRegistry()
        {
            this.Constructions = new Dictionary<string, string>();
        }
    }
}
