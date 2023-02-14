namespace Dry.AspNetCore.Infrastructure.AppConfigure;

/// <summary>
/// Dependency配置类
/// </summary>
public class DependencyConfigurer : IAppConfigurer, ISingletonDependency<IAppConfigurer>
{
    public int Order { get; set; } = int.MinValue;

    public virtual Task ConfigureAsync(WebApplication app)
    {
        IDependency.RootServiceProvider = app.Services;
        return Task.CompletedTask;
    }
}