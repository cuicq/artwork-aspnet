
namespace Artworks.Schedule
{
    /// <summary>
    /// 执行计划接口。
    /// </summary>
    public interface IScheduleChain
    {
        /// <summary>
        /// 计划名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 启动
        /// </summary>
        void Startup();

    }
}
