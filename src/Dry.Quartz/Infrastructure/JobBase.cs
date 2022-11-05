using Dry.Quartz.Model;
using Quartz;
using System.Threading.Tasks;

namespace Dry.Quartz.Infrastructure
{
    /// <summary>
    /// /作业基类
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public abstract class JobBase<TJobModel, TTriggerModel> : IJob
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        protected abstract Task Execute(IJobExecutionContext context, TJobModel job, TTriggerModel trigger);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task Execute(IJobExecutionContext context)
        {
            var job = context.JobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel;
            if (job is not null)
            {
                job.ExecutedCount++;
            }
            var trigger = context.Trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel;
            if (trigger is not null)
            {
                trigger.ExecutedCount++;
            }
            await Execute(context, job, trigger);
        }
    }
}