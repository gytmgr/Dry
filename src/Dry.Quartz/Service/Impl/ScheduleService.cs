using Dry.Dependency;
using Dry.Quartz.Model;
using Quartz;
using System.Threading.Tasks;

namespace Dry.Quartz.Service.Impl
{
    internal class ScheduleService : IScheduleService, IDependency<IScheduleService>
    {
        private readonly IScheduler _scheduler;

        public ScheduleService(IScheduler scheduler)
            => _scheduler = scheduler;

        public async Task StartAsync()
            => await _scheduler.Start();

        public async Task StandbyAsync()
            => await _scheduler.Standby();

        public async Task ShutdownAsync(bool waitForJobsToComplete = false)
            => await _scheduler.Shutdown(waitForJobsToComplete);

        public async Task PauseAsync()
            => await _scheduler.PauseAll();

        public async Task ResumeAsync()
            => await _scheduler.ResumeAll();

        public async Task ClearAsync()
            => await _scheduler.Clear();

        public async Task<SchedulerModel> GetAsync()
        {
            var metaData = await _scheduler.GetMetaData();
            return new SchedulerModel
            {
                Name = metaData.SchedulerName,
                Started = metaData.Started,
                InStandbyMode = metaData.InStandbyMode,
                Shutdown = metaData.Shutdown,
                Version = metaData.Version,
                RunningSince = metaData.RunningSince?.LocalDateTime,
                NumberOfJobsExecuted = metaData.NumberOfJobsExecuted
            };
        }
    }
}