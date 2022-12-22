namespace Dry.Quartz.Service;

/// <summary>
/// 调度监听服务接口
/// </summary>
public interface IScheduleListenService
{
    /// <summary>
    /// 添加
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TSchedulerListener"></typeparam>
    /// <param name="schedulerListener"></param>
    void Add<TJobModel, TTriggerModel, TSchedulerListener>(TSchedulerListener schedulerListener)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TSchedulerListener"></typeparam>
    /// <param name="schedulerListener"></param>
    /// <returns></returns>
    bool Remove<TJobModel, TTriggerModel, TSchedulerListener>(TSchedulerListener schedulerListener)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>;

    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <typeparam name="TSchedulerListener"></typeparam>
    /// <returns></returns>
    TSchedulerListener[] Get<TJobModel, TTriggerModel, TSchedulerListener>()
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>;
}