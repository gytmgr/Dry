namespace Dry.Quartz.Service.Impl;

internal class TriggerListenService : ITriggerListenService, IDependency<ITriggerListenService>
{
    private readonly IScheduler _scheduler;

    public TriggerListenService(IScheduler scheduler)
        => _scheduler = scheduler;

    public void Add<TJobModel, TTriggerModel, TTriggerListener>(TTriggerListener triggerListener, params QuartzKey[] triggerKeys)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.AddTriggerListener(triggerListener, triggerKeys.Select(x => KeyMatcher<TriggerKey>.KeyEquals(x.ToTriggerKey())).ToArray());

    public void Add<TJobModel, TTriggerModel, TTriggerListener>(TTriggerListener triggerListener, params string[] triggerGroups)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.AddTriggerListener(triggerListener, triggerGroups.Select(x => GroupMatcher<TriggerKey>.GroupEquals(x)).ToArray());

    public bool Remove(string name)
        => _scheduler.ListenerManager.RemoveTriggerListener(name);

    public void AddMatch(string name, params QuartzKey[] triggerKeys)
        => _scheduler.ListenerManager.SetTriggerListenerMatchers(name, triggerKeys.Select(x => KeyMatcher<TriggerKey>.KeyEquals(x.ToTriggerKey())).ToArray());

    public void AddMatch(string name, params string[] triggerGroups)
        => _scheduler.ListenerManager.SetTriggerListenerMatchers(name, triggerGroups.Select(x => GroupMatcher<TriggerKey>.GroupEquals(x)).ToArray());

    public void RemoveMatch(string name, QuartzKey triggerKey)
        => _scheduler.ListenerManager.RemoveTriggerListenerMatcher(name, KeyMatcher<TriggerKey>.KeyEquals(triggerKey.ToTriggerKey()));

    public void RemoveMatch(string name, string triggerGroup)
        => _scheduler.ListenerManager.RemoveTriggerListenerMatcher(name, GroupMatcher<TriggerKey>.GroupEquals(triggerGroup));

    public TTriggerListener[] Get<TJobModel, TTriggerModel, TTriggerListener>()
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.GetTriggerListeners().Select(x => x as TTriggerListener).ToArray();

    public TTriggerListener Get<TJobModel, TTriggerModel, TTriggerListener>(string name)
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
        where TTriggerListener : TriggerListenerBase<TJobModel, TTriggerModel>
        => _scheduler.ListenerManager.GetTriggerListener(name) as TTriggerListener;

    public bool IsMatch(string name, QuartzKey triggerKey)
    {
        var matchers = _scheduler.ListenerManager.GetTriggerListenerMatchers(name);
        if (matchers?.Count > 0)
        {
            return matchers.Any(x => x.IsMatch(triggerKey.ToTriggerKey()));
        }
        return false;
    }
}