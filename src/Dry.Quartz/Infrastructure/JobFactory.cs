namespace Dry.Quartz.Infrastructure;

internal class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var serviceScope = _serviceProvider.CreateScope();
        return serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        if (job is IDisposable disposable && disposable is not null)
        {
            disposable.Dispose();
        }
    }
}