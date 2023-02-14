namespace Dry.AspNetCore.Infrastructure.AppStartup;

/// <summary>
/// 启动过滤器
/// </summary>
public class StartupFilter : IStartupFilter, ISingletonDependency<IStartupFilter>
{
    protected readonly IServiceProvider _serviceProvider;

    public StartupFilter(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    public virtual Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        WebAppHelper.ServicesActionAsync<IAppStartuper>(_serviceProvider, async startup => await startup.StartupAsync()).Wait();
        return next;
    }
}