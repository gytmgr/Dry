using Dry.Quartz.Model;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace Dry.Quartz.Infrastructure
{
    /// <summary>
    /// 作业监听器基类
    /// </summary>
    /// <typeparam name="TJobModel"></typeparam>
    /// <typeparam name="TTriggerModel"></typeparam>
    public abstract class JobListenerBase<TJobModel, TTriggerModel> : IJobListener
        where TJobModel : JobModel
        where TTriggerModel : TriggerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            var job = context.JobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel;
            var trigger = context.Trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel;
            await JobToBeExecuted(context, job, trigger);
        }

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        protected virtual Task JobToBeExecuted(IJobExecutionContext context, TJobModel job, TTriggerModel trigger) => Task.CompletedTask;

        /// <summary>
        /// 被否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            var job = context.JobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel;
            var trigger = context.Trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel;
            await JobExecutionVetoed(context, job, trigger);
        }

        /// <summary>
        /// 被否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        protected virtual Task JobExecutionVetoed(IJobExecutionContext context, TJobModel job, TTriggerModel trigger) => Task.CompletedTask;

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            var job = context.JobDetail.JobDataMap.Get(JobModel.MapKey) as TJobModel;
            var trigger = context.Trigger.JobDataMap.Get(TriggerModel.MapKey) as TTriggerModel;
            await JobWasExecuted(context, job, trigger);
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        protected virtual Task JobWasExecuted(IJobExecutionContext context, TJobModel job, TTriggerModel trigger) => Task.CompletedTask;
    }
}