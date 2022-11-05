using Dry.Dependency;
using Dry.Quartz.Infrastructure;
using Dry.Quartz.Model;
using Quartz;
using Quartz.Impl.Matchers;
using System.Linq;

namespace Dry.Quartz.Service.Impl
{
    internal class JobListenService : IJobListenService, IDependency<IJobListenService>
    {
        private readonly IScheduler _scheduler;

        public JobListenService(IScheduler scheduler)
            => _scheduler = scheduler;

        public void Add<TJobModel, TTriggerModel, TJobListener>(TJobListener jobListener, params QuartzKey[] jobKeys)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TJobListener : JobListenerBase<TJobModel, TTriggerModel>
            => _scheduler.ListenerManager.AddJobListener(jobListener, jobKeys.Select(x => KeyMatcher<JobKey>.KeyEquals(x.ToJobKey())).ToArray());

        public void Add<TJobModel, TTriggerModel, TJobListener>(TJobListener jobListener, params string[] jobGroups)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TJobListener : JobListenerBase<TJobModel, TTriggerModel>
            => _scheduler.ListenerManager.AddJobListener(jobListener, jobGroups.Select(x => GroupMatcher<JobKey>.GroupEquals(x)).ToArray());

        public bool Remove(string name)
            => _scheduler.ListenerManager.RemoveJobListener(name);

        public void AddMatch(string name, params QuartzKey[] jobKeys)
            => _scheduler.ListenerManager.SetJobListenerMatchers(name, jobKeys.Select(x => KeyMatcher<JobKey>.KeyEquals(x.ToJobKey())).ToArray());

        public void AddMatch(string name, params string[] jobGroups)
            => _scheduler.ListenerManager.SetJobListenerMatchers(name, jobGroups.Select(x => GroupMatcher<JobKey>.GroupEquals(x)).ToArray());

        public void RemoveMatch(string name, QuartzKey jobKey)
            => _scheduler.ListenerManager.RemoveJobListenerMatcher(name, KeyMatcher<JobKey>.KeyEquals(jobKey.ToJobKey()));

        public void RemoveMatch(string name, string jobGroup)
            => _scheduler.ListenerManager.RemoveJobListenerMatcher(name, GroupMatcher<JobKey>.GroupEquals(jobGroup));

        public TJobListener[] Get<TJobModel, TTriggerModel, TJobListener>()
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TJobListener : JobListenerBase<TJobModel, TTriggerModel>
            => _scheduler.ListenerManager.GetJobListeners().Select(x => x as TJobListener).ToArray();

        public TJobListener Get<TJobModel, TTriggerModel, TJobListener>(string name)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TJobListener : JobListenerBase<TJobModel, TTriggerModel>
            => _scheduler.ListenerManager.GetJobListener(name) as TJobListener;

        public bool IsMatch(string name, QuartzKey jobKey)
        {
            var matchers = _scheduler.ListenerManager.GetJobListenerMatchers(name);
            if (matchers?.Count > 0)
            {
                return matchers.Any(x => x.IsMatch(jobKey.ToJobKey()));
            }
            return false;
        }
    }
}