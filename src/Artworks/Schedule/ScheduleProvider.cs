using System;
using Artworks.Anomaly;
using Artworks.Schedule.CommonModel;

namespace Artworks.Schedule
{
    /// <summary>
    /// 执行计划处理基类。
    /// </summary>
    public abstract class ScheduleProvider : IScheduleChain, IDisposable
    {
        /// <summary>
        /// 执行计划名称。
        /// </summary>
        public string Name { get; private set; }

        public ScheduleProvider(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 执行。
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// 启动
        /// </summary>
        public void Startup()
        {
            try
            {
                this.Execute();
            }
            catch (Exception ex)
            {
                ExceptionManager.HandleException(new ScheduleException(ex.Message, ex));
            }
        }

        /// <summary>
        /// 释放资源。
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}
