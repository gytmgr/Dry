using Dry.Dependency;
using Dry.Quartz;
using Dry.Quartz.Infrastructure;
using Dry.Quartz.Model;
using Dry.Quartz.Service;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dry.Console.Test.Demo;

public static class Quartz
{
    public static async Task Run()
    {
        try
        {
            var now = DateTime.Now;
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDependency();
            serviceCollection.AddCustomQuartz();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var test1 = serviceProvider.GetService<ITest1>();
            var test2 = serviceProvider.GetService<ITest2>();

            var gg = serviceProvider.GetService<GG>();

            var scheduleListenService = serviceProvider.GetService<IScheduleListenService>();
            var schedulerListener = serviceProvider.GetService<SchedulerListener>();
            scheduleListenService.Add<JobModel, TriggerModel, SchedulerListener>(schedulerListener);
            var jobListenService = serviceProvider.GetService<IJobListenService>();
            var jobListener = serviceProvider.GetService<JobListener>();
            jobListenService.Add<JobModel, TriggerModel, JobListener>(jobListener, "Group1", "Group2");
            var triggerListenService = serviceProvider.GetService<ITriggerListenService>();
            var triggerListener = serviceProvider.GetService<TriggerListener>();
            triggerListenService.Add<JobModel, TriggerModel, TriggerListener>(triggerListener, "Group1", "Group2", "Group3");

            var scheduleService = serviceProvider.GetService<IScheduleService>();
            var jobService = serviceProvider.GetService<IJobService>();
            var triggerService = serviceProvider.GetService<ITriggerService>();

            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度启动");
            await scheduleService.StartAsync();

            var jobKey1 = new QuartzKey { Name = "Job1", Group = "Group1" };
            var triggerKey1 = new QuartzKey { Name = "Trigger1", Group = "Group1" };
            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业1新增");
            await jobService.AddAsync<JobModel, TriggerModel, JobGroup1>(new JobModel
            {
                Key = jobKey1
            }, new CalendarTriggerModel
            {
                Key = triggerKey1,
                //StartTime = now.AddDays(-1).AddSeconds(-3),
                Unit = IntervalUnit.Second,
                Interval = 5,
                EndTime = now.AddSeconds(20.1)
            });

            await Task.Delay(TimeSpan.FromSeconds(2));
            await scheduleService.StandbyAsync();

            await Task.Delay(TimeSpan.FromSeconds(10));
            await scheduleService.StartAsync();
        }
        catch (Exception e)
        {

            throw;
        }
        System.Console.ReadKey();
    }
}

public class JobGroup1 : JobBase<JobModel, TriggerModel>
{
    protected override Task Execute(IJobExecutionContext context, JobModel Job, TriggerModel trigger)
    {
        if (Job.Key.Name == "Job1" && Job.ExecutedCount == 1)
        {
            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 手动报错：Job【{Job.Key.Name}，{Job.Key.Group}】 Trigger【{trigger?.Key.Name}，{trigger?.Key.Group}】 MisfireInstruction【{context.Trigger?.MisfireInstruction}】");
            throw new Exception($"Job【{Job.Key.Name}，{Job.Key.Group}】 Trigger【{trigger?.Key.Name}，{trigger?.Key.Group}】 出错");
        }
        else
        {
            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Job【{Job.Key.Name}，{Job.Key.Group}】 Trigger【{trigger?.Key.Name}，{trigger?.Key.Group}】 MisfireInstruction【{context.Trigger?.MisfireInstruction}】");
        }
        return Task.CompletedTask;
    }
}

public class JobGroup2 : JobBase<JobModel, TriggerModel>
{
    protected override Task Execute(IJobExecutionContext context, JobModel Job, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Job【{Job.Key.Name}，{Job.Key.Group}】 Trigger【{trigger?.Key.Name}，{trigger?.Key.Group}】 MisfireInstruction【{context.Trigger?.MisfireInstruction}】");
        return Task.CompletedTask;
    }
}

public class SchedulerListener : SchedulerListenerBase<JobModel, TriggerModel>
{
    public SchedulerListener(IServiceProvider serviceProvider)
    {
        System.Console.WriteLine($"{Guid.NewGuid()} 调度初始化");
    }

    public override Task SchedulerStarting(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度开启前");
        return base.SchedulerStarting(cancellationToken);
    }

    public override Task SchedulerStarted(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度开启后");
        return base.SchedulerStarted(cancellationToken);
    }

    public override Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度进入待机模式");
        return base.SchedulerInStandbyMode(cancellationToken);
    }

    public override Task SchedulingDataCleared(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度清除后");
        return base.SchedulingDataCleared(cancellationToken);
    }

    public override Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度停止前");
        return base.SchedulerShuttingdown(cancellationToken);
    }

    public override Task SchedulerShutdown(CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度停止后");
        return base.SchedulerShutdown(cancellationToken);
    }

    public override Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 调度错误【Msg:{cause.InnerException?.Message}】");
        return base.SchedulerError(msg, cause, cancellationToken);
    }

    protected override Task JobAdded(IJobDetail jobDetail, JobModel job)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业添加后：Job【{job.Key.Name},{job.Key.Group}】");
        return base.JobAdded(jobDetail, job);
    }

    protected override Task JobScheduled(ITrigger quartzTrigger, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业添加触发器调度后：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.JobScheduled(quartzTrigger, trigger);
    }

    public override Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业解除触发器调度后：Trigger【{triggerKey.Name},{triggerKey.Group}】");
        return base.JobUnscheduled(triggerKey, cancellationToken);
    }

    public override Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业暂停后：Job【{jobKey.Name},{jobKey.Group}】");
        return base.JobPaused(jobKey, cancellationToken);
    }

    public override Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业组暂停后：JobGroup【{jobGroup}】");
        return base.JobsPaused(jobGroup, cancellationToken);
    }

    public override Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业恢复后：Job【{jobKey.Name},{jobKey.Group}】");
        return base.JobResumed(jobKey, cancellationToken);
    }

    public override Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业组恢复后：JobGroup【{jobGroup}】");
        return base.JobsResumed(jobGroup, cancellationToken);
    }

    public override Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业打断后：Job【{jobKey.Name},{jobKey.Group}】");
        return base.JobInterrupted(jobKey, cancellationToken);
    }

    public override Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业删除后：Job【{jobKey.Name},{jobKey.Group}】");
        return base.JobDeleted(jobKey, cancellationToken);
    }

    public override Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器暂停后：Trigger【{triggerKey.Name},{triggerKey.Group}】");
        return base.TriggerPaused(triggerKey, cancellationToken);
    }

    public override Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器组暂停后：TriggerGroup【{triggerGroup}】");
        return base.TriggersPaused(triggerGroup, cancellationToken);
    }

    public override Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器恢复后：Trigger【{triggerKey.Name},{triggerKey.Group}】");
        return base.TriggerResumed(triggerKey, cancellationToken);
    }

    public override Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器组恢复后：TriggerGroup【{triggerGroup}】");
        return base.TriggersResumed(triggerGroup, cancellationToken);
    }

    protected override Task TriggerFinalized(ITrigger quartzTrigger, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器完成后：Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.TriggerFinalized(quartzTrigger, trigger);
    }
}

public class JobListener : JobListenerBase<JobModel, TriggerModel>
{
    public override string Name => "JobListener";

    protected override Task JobToBeExecuted(IJobExecutionContext context, JobModel job, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业执行前：Job【{job.Key.Name},{job.Key.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.JobToBeExecuted(context, job, trigger);
    }

    protected override Task JobExecutionVetoed(IJobExecutionContext context, JobModel job, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业被否决：Job【{job.Key.Name},{job.Key.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.JobExecutionVetoed(context, job, trigger);
    }

    protected override Task JobWasExecuted(IJobExecutionContext context, JobModel job, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 作业执行后：Job【{job.Key.Name},{job.Key.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.JobWasExecuted(context, job, trigger);
    }
}

public class TriggerListener : TriggerListenerBase<JobModel, TriggerModel>
{
    public override string Name => "TriggerListener";

    protected override Task TriggerFired(ITrigger quartzTrigger, TriggerModel trigger, IJobExecutionContext context)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器触发后：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.TriggerFired(quartzTrigger, trigger, context);
    }

    private static bool _exe = false;

    protected override Task<bool> VetoJobExecution(ITrigger quartzTrigger, TriggerModel trigger, IJobExecutionContext context)
    {
        if (trigger.Key.Name == "Trigger1" && trigger.ExecutedCount == 3 && !_exe)
        {
            _exe = true;
            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器否决触发（否决）：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
            return Task.FromResult(true);
        }
        else
        {
            System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器否决触发：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        }
        return base.VetoJobExecution(quartzTrigger, trigger, context);
    }

    protected override Task TriggerMisfired(ITrigger quartzTrigger, TriggerModel trigger)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器触发丢失后：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.TriggerMisfired(quartzTrigger, trigger);
    }

    protected override Task TriggerComplete(ITrigger quartzTrigger, TriggerModel trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
    {
        System.Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 触发器完成：Job【{trigger.JobKey.Name},{trigger.JobKey.Group}】，Trigger【{trigger.Key.Name},{trigger.Key.Group}】");
        return base.TriggerComplete(quartzTrigger, trigger, context, triggerInstructionCode);
    }
}

public interface ITest1 { };
public interface ITest2 { };
public class Test : ITest1, ISingletonDependency<ITest1>, ITest2, ISingletonDependency<ITest2>
{
    public virtual ITest2 GG { get; }
    public Test()
    {
        if (this is ITest1)
        {

        }
    }
};
public class TTest : Test
{
    public override TTest GG { get; }
}