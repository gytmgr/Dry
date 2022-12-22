namespace Dry.Quartz.Service.Impl;

internal class ScheduleListenService : IScheduleListenService, IDependency<IScheduleListenService>
{
    private readonly IScheduler _scheduler;

    public ScheduleListenService(IScheduler scheduler)
        => _scheduler = scheduler;

    public void Add<TJobModel, TTriggerModel, TSchedulerListener>(TSchedulerListener schedulerListener)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.AddSchedulerListener(schedulerListener);

    public bool Remove<TJobModel, TTriggerModel, TSchedulerListener>(TSchedulerListener schedulerListener)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.RemoveSchedulerListener(schedulerListener);

    public TSchedulerListener[] Get<TJobModel, TTriggerModel, TSchedulerListener>()
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TSchedulerListener : SchedulerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.GetSchedulerListeners().Select(x => x as TSchedulerListener).ToArray();
}