namespace Dry.Quartz.Service.Impl;

internal class TriggerService : ITriggerService, IDependency<ITriggerListenService>
{
    private readonly IScheduler _scheduler;

    public TriggerService(IScheduler scheduler)
        => _scheduler = scheduler;

    public async Task<bool> AnyAsync(QuartzKey key)
        => await _scheduler.CheckExists(key.ToTriggerKey());

    public async Task<bool> AddAsync<TTriggerModel>(QuartzKey jobKey, TTriggerModel trigger)
        where TTriggerModel : TriggerModel
    {
        var msg = trigger.Check();
        if (msg is not null)
        {
            throw new AggregateException(msg);
        }
        var quartzTrigger = trigger.BuildTrigger(jobKey);
        if (await _scheduler.CheckExists(quartzTrigger.JobKey) && !await _scheduler.CheckExists(quartzTrigger.Key))
        {
            trigger.JobKey = jobKey;
            await _scheduler.ScheduleJob(quartzTrigger);
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(QuartzKey key)
        => await _scheduler.UnscheduleJob(key.ToTriggerKey());

    public async Task PauseAsync(QuartzKey key)
        => await _scheduler.PauseTrigger(key.ToTriggerKey());

    public async Task PauseAsync(string group)
         => await _scheduler.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals(group));

    public async Task ResumeAsync(QuartzKey key)
        => await _scheduler.ResumeTrigger(key.ToTriggerKey());

    public async Task ResumeAsync(string group)
         => await _scheduler.ResumeTriggers(GroupMatcher<TriggerKey>.GroupEquals(group));

    public async Task<bool> ReplaceAsync<TTriggerModel>(QuartzKey oldTriggerKey, TTriggerModel newTrigger)
        where TTriggerModel : TriggerModel
    {
        var msg = newTrigger.Check();
        if (msg is not null)
        {
            throw new AggregateException(msg);
        }
        var quartzTriggerKey = oldTriggerKey.ToTriggerKey();
        var quartzTrigger = await _scheduler.GetTrigger(quartzTriggerKey);
        if (quartzTrigger is not null)
        {
            var quartzTtrigger = newTrigger.BuildTrigger(new QuartzKey
            {
                Name = quartzTrigger.JobKey.Name,
                Group = quartzTrigger.JobKey.Group
            });
            await _scheduler.RescheduleJob(quartzTriggerKey, quartzTtrigger);
            return true;
        }
        return false;
    }

    public async Task<TTriggerModel[]> GetAsync<TTriggerModel>()
        where TTriggerModel : TriggerModel, new()
    {
        var triggers = new Collection<TTriggerModel>();
        var groupNames = await _scheduler.GetJobGroupNames();
        foreach (var groupName in groupNames)
        {
            var keys = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(groupName));
            foreach (var key in keys)
            {
                var quartzTrigger = await _scheduler.GetTrigger(key);
                if (quartzTrigger is not null)
                {
                    var trigger = quartzTrigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel ?? new TTriggerModel
                    {
                        Key = new QuartzKey
                        {
                            Name = quartzTrigger.Key.Name,
                            Group = quartzTrigger.Key.Group
                        },
                        JobKey = new QuartzKey
                        {
                            Name = quartzTrigger.JobKey.Name,
                            Group = quartzTrigger.JobKey.Group
                        },
                        Description = quartzTrigger.Description,
                        State = await _scheduler.GetTriggerState(quartzTrigger.Key)
                    };
                    triggers.Add(trigger);
                }
            }
        }
        return triggers.ToArray();
    }

    public async Task<TTriggerModel[]> GetByGroupAsync<TTriggerModel>(string group)
        where TTriggerModel : TriggerModel, new()
    {
        var triggers = new Collection<TTriggerModel>();
        var keys = await _scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.GroupEquals(group));
        foreach (var key in keys)
        {
            var quartzTrigger = await _scheduler.GetTrigger(key);
            if (quartzTrigger is not null)
            {
                var trigger = quartzTrigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel ?? new TTriggerModel
                {
                    Key = new QuartzKey
                    {
                        Name = quartzTrigger.Key.Name,
                        Group = quartzTrigger.Key.Group
                    },
                    JobKey = new QuartzKey
                    {
                        Name = quartzTrigger.JobKey.Name,
                        Group = quartzTrigger.JobKey.Group
                    },
                    Description = quartzTrigger.Description,
                    State = await _scheduler.GetTriggerState(quartzTrigger.Key)
                };
                triggers.Add(trigger);
            }
        }
        return triggers.ToArray();
    }

    public async Task<TTriggerModel> GetAsync<TTriggerModel>(QuartzKey key)
        where TTriggerModel : TriggerModel, new()
    {
        var quartzTrigger = await _scheduler.GetTrigger(key.ToTriggerKey());
        if (quartzTrigger is not null)
        {
            return quartzTrigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel ?? new TTriggerModel
            {
                Key = new QuartzKey
                {
                    Name = quartzTrigger.Key.Name,
                    Group = quartzTrigger.Key.Group
                },
                JobKey = new QuartzKey
                {
                    Name = quartzTrigger.JobKey.Name,
                    Group = quartzTrigger.JobKey.Group
                },
                Description = quartzTrigger.Description,
                State = await _scheduler.GetTriggerState(quartzTrigger.Key)
            };
        }
        return null;
    }

    public async Task<TriggerState> GetStateAsync(QuartzKey key)
        => await _scheduler.GetTriggerState(key.ToTriggerKey());
}