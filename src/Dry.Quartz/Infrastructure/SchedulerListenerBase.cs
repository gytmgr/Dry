namespace Dry.Quartz.Infrastructure;

/// <summary>
/// 调度监听器基类
/// </summary>
/// <typeparam name="TJobModel"></typeparam>
/// <typeparam name="TTriggerModel"></typeparam>
public abstract class SchedulerListenerBase<TJobModel, TTriggerModel> : ISchedulerListener
    where TJobModel : JobModel
    where TTriggerModel : TriggerModel
{
    #region 调度

    /// <summary>
    /// 调度开启前
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerStarting(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度开启后
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerStarted(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度进入待机模式
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerInStandbyMode(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度清除后
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task SchedulingDataCleared(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度停止前
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerShuttingdown(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度停止后
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerShutdown(CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 调度错误
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="cause"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default) => Task.CompletedTask;

    #endregion

    #region 作业

    /// <summary>
    /// 作业添加后
    /// </summary>
    /// <param name="jobDetail"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        => await JobAdded(jobDetail, jobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel);

    /// <summary>
    /// 作业添加后
    /// </summary>
    /// <param name="jobDetail"></param>
    /// <param name="job"></param>
    /// <returns></returns>
    protected virtual Task JobAdded(IJobDetail jobDetail, TJobModel? job) => Task.CompletedTask;

    /// <summary>
    /// 作业添加触发器调度后
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        => await JobScheduled(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel);

    /// <summary>
    /// 作业添加触发器调度后
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <returns></returns>
    protected virtual Task JobScheduled(ITrigger quartzTrigger, TTriggerModel? trigger) => Task.CompletedTask;

    /// <summary>
    /// 作业解除触发器调度后
    /// </summary>
    /// <param name="triggerKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业暂停后
    /// </summary>
    /// <param name="jobKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业组暂停后
    /// </summary>
    /// <param name="jobGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业恢复后
    /// </summary>
    /// <param name="jobKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业组恢复后
    /// </summary>
    /// <param name="jobGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业打断后
    /// </summary>
    /// <param name="jobKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 作业删除后
    /// </summary>
    /// <param name="jobKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    #endregion

    #region 触发器

    /// <summary>
    /// 触发器暂停后
    /// </summary>
    /// <param name="triggerKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 触发器组暂停后
    /// </summary>
    /// <param name="triggerGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task TriggersPaused(string? triggerGroup, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 触发器恢复后
    /// </summary>
    /// <param name="triggerKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 触发器组恢复后
    /// </summary>
    /// <param name="triggerGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task TriggersResumed(string? triggerGroup, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <summary>
    /// 触发器结束后
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        => await TriggerFinalized(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel);

    /// <summary>
    /// 触发器结束后
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <returns></returns>
    protected virtual Task TriggerFinalized(ITrigger quartzTrigger, TTriggerModel? trigger) => Task.CompletedTask;

    #endregion
}