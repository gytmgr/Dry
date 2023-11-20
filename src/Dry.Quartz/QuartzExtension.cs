global using Dry.Core.Model;
global using Dry.Dependency;
global using Dry.Quartz.Infrastructure;
global using Dry.Quartz.Model;
global using Microsoft.Extensions.DependencyInjection;
global using Quartz;
global using Quartz.Impl;
global using Quartz.Impl.Matchers;
global using Quartz.Spi;
global using System.Collections.ObjectModel;

namespace Dry.Quartz;

/// <summary>
/// Quartz
/// </summary>
public static class QuartzExtension
{
    /// <summary>
    /// 添加Quartz扩展服务注册
    /// </summary>
    /// <param name="services"></param>
    /// <param name="prefixs">程序集名称前缀</param>
    /// <returns></returns>
    public static IServiceCollection AddCustomQuartz(this IServiceCollection services, params string[]? prefixs)
    {
        services.AddSingleton<IJobFactory, JobFactory>();
        services.AddSingleton(serviceProvider =>
        {
            var factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.JobFactory = serviceProvider.GetService<IJobFactory>()!;
            return scheduler;
        });

        var prefixList = new string[] { "Dry." };
        if (prefixs is not null)
        {
            prefixList = prefixList.Union(prefixs).ToArray();
        }
        var jobType = typeof(IJob);
        var types = AssemblyHelper.GetAll(prefixList)
            .SelectMany(x => x.DefinedTypes)
            .Select(x => x.AsType())
            .Where(x => x != jobType && jobType.IsAssignableFrom(x))
            .Where(x => x.IsClass && !x.IsAbstract)
            .ToArray();
        foreach (var type in types)
        {
            services.AddTransient(type);
        }
        return services;
    }

    /// <summary>
    /// 创建触发器
    /// </summary>
    /// <typeparam name="TTriggerModel"></typeparam>
    /// <param name="trigger"></param>
    /// <param name="jobKey"></param>
    /// <returns></returns>
    internal static ITrigger BuildTrigger<TTriggerModel>(this TTriggerModel trigger, QuartzKey jobKey) where TTriggerModel : TriggerModel
    {
        trigger.JobKey = jobKey;
        var triggerKey = trigger.Key.ToTriggerKey();
        var triggerBuilder = TriggerBuilder.Create().WithIdentity(triggerKey).WithDescription(trigger.Description)
            .ForJob(jobKey.ToJobKey());
        switch (trigger)
        {
            case SimpleTriggerModel simpleTrigger:
                triggerBuilder = triggerBuilder.WithSimpleSchedule(x => x.WithInterval(simpleTrigger.Interval).WithRepeatCount(simpleTrigger.RepeatCount));
                break;
            case DailyTriggerModel dailyTimeIntervalTrigger:
                triggerBuilder = triggerBuilder.WithDailyTimeIntervalSchedule(x =>
                {
                    x.WithIntervalInSeconds(dailyTimeIntervalTrigger.IntervalSecond).WithRepeatCount(dailyTimeIntervalTrigger.RepeatCount);
                    if (dailyTimeIntervalTrigger.DayOfWeeks?.Length > 0)
                    {
                        x.OnDaysOfTheWeek(dailyTimeIntervalTrigger.DayOfWeeks);
                    }
                    else
                    {
                        x.OnEveryDay();
                    }
                    if (dailyTimeIntervalTrigger.StartTimeOfDay is not null)
                    {
                        x.StartingDailyAt(dailyTimeIntervalTrigger.StartTimeOfDay);
                        if (dailyTimeIntervalTrigger.CountOfDay.HasValue)
                        {
                            x.EndingDailyAfterCount(dailyTimeIntervalTrigger.CountOfDay.Value);
                        }
                    }
                    if (dailyTimeIntervalTrigger.EndTimeOfDay is not null)
                    {
                        x.EndingDailyAt(dailyTimeIntervalTrigger.EndTimeOfDay);
                    }
                });
                break;
            case CalendarTriggerModel calendarTrigger:
                triggerBuilder = triggerBuilder.WithCalendarIntervalSchedule(x => x.WithInterval(calendarTrigger.Interval, calendarTrigger.Unit));
                break;
            case CronTriggerModel cronTrigger:
                triggerBuilder = triggerBuilder.WithCronSchedule(cronTrigger.CronExpression!);
                break;
        }
        if (trigger.StartTime.HasValue)
        {
            triggerBuilder = triggerBuilder.StartAt(trigger.StartTime.Value);
        }
        if (trigger.EndTime.HasValue)
        {
            triggerBuilder = triggerBuilder.EndAt(trigger.EndTime.Value);
        }
        var quartzTrigger = triggerBuilder.Build();
        quartzTrigger.JobDataMap.Add(TriggerModel.MapKey, trigger);
        return quartzTrigger;
    }

    /// <summary>
    /// 转作业主键
    /// </summary>
    /// <param name="jobKey"></param>
    /// <returns></returns>
    internal static JobKey ToJobKey(this QuartzKey jobKey)
        => new(jobKey.Name, jobKey.Group);

    /// <summary>
    /// 转触发器主键
    /// </summary>
    /// <param name="triggerKey"></param>
    /// <returns></returns>
    internal static TriggerKey ToTriggerKey(this QuartzKey triggerKey)
        => new(triggerKey.Name, triggerKey.Group);
}