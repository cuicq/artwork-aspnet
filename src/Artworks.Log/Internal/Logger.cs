using System.Collections.Generic;

namespace Artworks.Log.Internal
{
    /// <summary>
    /// 日志。
    /// </summary>
    internal class Logger
    {
        private static Dictionary<string, ILog> container = new Dictionary<string, ILog>();

        /// <summary>
        /// 获取日志存储器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetLogger(string name)
        {
            ILog logger = null;

            if (!container.ContainsKey(name))
            {
                logger = new MyLog(name);
                container.Add(name, logger);
            }
            else
            {
                logger = container[name];
            }

            return logger;
        }


    }
}
