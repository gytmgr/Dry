using Dry.Dependency;
using Dry.Quartz.Infrastructure;
using Dry.Quartz.Model;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dry.Quartz.Service.Impl
{
    internal class JobService : IJobService, IDependency<IJobService>
    {
        private readonly IScheduler _scheduler;

        public JobService(IScheduler scheduler)
            => _scheduler = scheduler;

        public async Task<bool> AnyAsync(QuartzKey key)
            => await _scheduler.CheckExists(key.ToJobKey());

        public async Task<bool> AddAsync<TJobModel, TTriggerModel, TJob>(TJobModel job, params TTriggerModel[] triggers)
            where TJobModel : JobModel
            where TTriggerModel : TriggerModel
            where TJob : JobBase<TJobModel, TTriggerModel>
        {
            if (triggers.Any(x => x.Interval == default && x.RepeatCount != 0))
            {
                throw new AggregateException("执行多次时，必须有间隔时间");
            }
            var quartzJobKey = job.Key.ToJobKey();
            if (!await _scheduler.CheckExists(quartzJobKey))
            {
                var jobDetail = JobBuilder.Create<TJob>().WithIdentity(quartzJobKey).WithDescription(job.Description).Build();
                jobDetail.JobDataMap.Add(JobModel.MapKey, job);

                var quartzTriggers = triggers.Select(x => x.BuildTrigger(job.Key)).ToList();
                await _scheduler.ScheduleJob(jobDetail, new ReadOnlyCollection<ITrigger>(quartzTriggers), false);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(QuartzKey key)
            => await _scheduler.DeleteJob(key.ToJobKey());

        public async Task PauseAsync(QuartzKey key)
            => await _scheduler.PauseJob(key.ToJobKey());

        public async Task PauseAsync(string group)
             => await _scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(group));

        public async Task ResumeAsync(QuartzKey key)
         => await _scheduler.ResumeJob(key.ToJobKey());

        public async Task ResumeAsync(string group)
             => await _scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(group));

        public async Task<bool> ExecuteAsync<TJobModel>(TJobModel job)
            where TJobModel : JobModel
        {
            var quartzJobKey = job.Key.ToJobKey();
            if (await _scheduler.CheckExists(quartzJobKey))
            {
                var JobDataMap = new JobDataMap();
                JobDataMap.Add(JobModel.MapKey, job);
                await _scheduler.TriggerJob(quartzJobKey, JobDataMap);
                return true;
            }
            return false;
        }

        public async Task<TJobModel[]> GetAsync<TJobModel>()
            where TJobModel : JobModel, new()
        {
            var jobs = new Collection<TJobModel>();
            var groupNames = await _scheduler.GetJobGroupNames();
            foreach (var groupName in groupNames)
            {
                var keys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName));
                foreach (var key in keys)
                {
                    var jobDetail = await _scheduler.GetJobDetail(key);
                    if (jobDetail is not null)
                    {
                        var job = jobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel ?? new TJobModel
                        {
                            Key = new QuartzKey
                            {
                                Name = jobDetail.Key.Name,
                                Group = jobDetail.Key.Group
                            },
                            Description = jobDetail.Description
                        };
                        jobs.Add(job);
                    }
                }
            }
            return jobs.ToArray();
        }

        public async Task<TJobModel[]> GetByGroupAsync<TJobModel>(string group)
            where TJobModel : JobModel, new()
        {
            var jobs = new Collection<TJobModel>();
            var keys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(group));
            foreach (var key in keys)
            {
                var jobDetail = await _scheduler.GetJobDetail(key);
                if (jobDetail is not null)
                {
                    var job = jobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel ?? new TJobModel
                    {
                        Key = new QuartzKey
                        {
                            Name = jobDetail.Key.Name,
                            Group = jobDetail.Key.Group
                        },
                        Description = jobDetail.Description
                    };
                    jobs.Add(job);
                }
            }
            return jobs.ToArray();
        }

        public async Task<TJobModel> GetAsync<TJobModel>(QuartzKey key)
            where TJobModel : JobModel, new()
        {
            var jobDetail = await _scheduler.GetJobDetail(key.ToJobKey());
            if (jobDetail is not null)
            {
                return jobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel ?? new TJobModel
                {
                    Key = new QuartzKey
                    {
                        Name = jobDetail.Key.Name,
                        Group = jobDetail.Key.Group
                    },
                    Description = jobDetail.Description
                };
            }
            return null;
        }

        public async Task<TTriggerModel[]> GetTriggerAsync<TTriggerModel>(QuartzKey jobKey)
            where TTriggerModel : TriggerModel, new()
        {
            var triggers = new Collection<TTriggerModel>();
            var quartzTriggers = await _scheduler.GetTriggersOfJob(jobKey.ToJobKey());
            foreach (var quartzTrigger in quartzTriggers)
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
                    Description = quartzTrigger.Description
                };
                trigger.State = await _scheduler.GetTriggerState(quartzTrigger.Key);
                triggers.Add(trigger);
            }
            return triggers.ToArray();
        }
    }
}