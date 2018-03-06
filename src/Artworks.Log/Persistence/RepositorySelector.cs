using Artworks.Log.CommonModel;
using Artworks.Log.Configuration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artworks.Log.Persistence
{
    /// <summary>
    /// 日志存储器。
    /// </summary>
    internal class RepositorySelector
    {
        private static Dictionary<string, ILoggerRepository> container = new Dictionary<string, ILoggerRepository>();

        /// <summary>
        /// 获取日志仓储
        /// </summary>
        public static ILoggerRepository GetRepository(string name)
        {
            ILoggerRepository repository = null;
            if (container.ContainsKey(name))
            {
                repository = container[name];
            }
            else
            {
                repository = CreateRepository(name);
                container.Add(name, repository);
            }
            return repository;
        }

        private static ILoggerRepository CreateRepository(string name)
        {
            var target = LogRegistryConfiguration.Instance.GetValue<LogRegistry>(name);
            if (target == null)
            {
                throw new System.Exception(string.Format("Not found the {0} log components。", name));
            }

            LogRegistry registry = (LogRegistry)target;
            Type type = Type.GetType(registry.Repository);
            object obj = null;
            if (registry.Constructions.Count > 0)
            {
                obj = Activator.CreateInstance(type, registry.Constructions.Values.ToArray());
            }
            else
            {
                obj = Activator.CreateInstance(type);
            }

            if (obj == null) throw new System.Exception(string.Format("Failed to create log {0} instance objects。", name));
            return (ILoggerRepository)obj;
        }

    }
}
