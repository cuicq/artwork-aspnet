using System;
using System.Linq;
using Artworks.Anomaly;
using Artworks.Schedule.CommonModel;
using Artworks.Schedule.Configuration;
using System.Collections.Generic;
using Artworks.Schedule.Configuration.Internal;

namespace Artworks.Schedule
{
    /// <summary>
    /// 执行计划管理。
    /// </summary>
    public sealed class ScheduleManager
    {
        private static Dictionary<string, IScheduleChain> scheduleChains = new Dictionary<string, IScheduleChain>();
        private static ScheduleManager instanceObj = null;
        private static object lockObj = new object();

        /// <summary>
        /// 实例
        /// </summary>
        public static ScheduleManager Instance
        {
            get
            {
                if (instanceObj == null)
                {
                    lock (lockObj)
                    {
                        if (instanceObj == null)
                        {
                            instanceObj = new ScheduleManager();
                        }
                    }
                }
                return instanceObj;
            }
        }

        private ScheduleManager()
        {
            try
            {
                var scheduleRegistries = ScheduleRegistryConfiguration.Instance.GetValues<ScheduleRegistry>();
                foreach (ScheduleRegistry registry in scheduleRegistries)
                {
                    var type = Type.GetType(registry.Type);
                    if (type.GetInterface("IScheduleChain") != null && !type.IsAbstract)
                    {
                        IScheduleChain scheduleChain = (IScheduleChain)Activator.CreateInstance(type, new string[] { registry.Name });
                        scheduleChains.Add(registry.Name, scheduleChain);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(new ScheduleException(ex.Message, ex));
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Startup()
        {
            foreach (IScheduleChain schedule in scheduleChains.Values.AsEnumerable())
            {
                try
                {
                    schedule.Startup();
                }
                catch (System.Exception ex)
                {
                    ExceptionManager.HandleException<ScheduleException>(new ScheduleException(schedule.Name, ex));
                }
            }
        }
    }
}
