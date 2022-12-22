namespace Dry.Quartz.Infrastructure;

/// <summary>
/// 触发器监听器基类
/// </summary>
/// <typeparam name="TJobModel"></typeparam>
/// <typeparam name="TTriggerModel"></typeparam>
public abstract class TriggerListenerBase<TJobModel, TTriggerModel> : ITriggerListener
    where TJobModel : JobModel
    where TTriggerModel : TriggerModel
{
    /// <summary>
    /// 名称
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 触发后
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    => await TriggerFired(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel, context);

    /// <summary>
    /// 触发后
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected virtual Task TriggerFired(ITrigger quartzTrigger, TTriggerModel trigger, IJobExecutionContext context) => Task.CompletedTask;

    /// <summary>
    /// 否决触发
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    => await VetoJobExecution(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel, context);

    /// <summary>
    /// 否决触发
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected virtual Task<bool> VetoJobExecution(ITrigger quartzTrigger, TTriggerModel trigger, IJobExecutionContext context) => Task.FromResult(false);

    /// <summary>
    /// 触发丢失后
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        => await TriggerMisfired(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel);

    /// <summary>
    /// 触发丢失后
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <returns></returns>
    protected virtual Task TriggerMisfired(ITrigger quartzTrigger, TTriggerModel trigger) => Task.CompletedTask;

    /// <summary>
    /// 完成
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <param name="triggerInstructionCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
    => await TriggerComplete(trigger, trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel, context, triggerInstructionCode);

    /// <summary>
    /// 完成
    /// </summary>
    /// <param name="quartzTrigger"></param>
    /// <param name="trigger"></param>
    /// <param name="context"></param>
    /// <param name="triggerInstructionCode"></param>
    /// <returns></returns>
    protected virtual Task TriggerComplete(ITrigger quartzTrigger, TTriggerModel trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode) => Task.CompletedTask;
}