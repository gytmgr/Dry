using Dry.Quartz.Model;
using System.Threading.Tasks;

namespace Dry.Quartz.Service
{
    /// <summary>
    /// 调度服务接口
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>
        /// 开启
        /// </summary>
        /// <returns></returns>
        Task StartAsync();

        /// <summary>
        /// 待机
        /// </summary>
        /// <returns></returns>
        Task StandbyAsync();

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="waitForJobsToComplete"></param>
        /// <returns></returns>
        Task ShutdownAsync(bool waitForJobsToComplete = false);

        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        Task PauseAsync();

        /// <summary>
        /// 恢复
        /// </summary>
        /// <returns></returns>
        Task ResumeAsync();

        /// <summary>
        /// 清除
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<SchedulerModel> GetAsync();
    }
}