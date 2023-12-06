namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 服务配置类
/// </summary>
public class ServiceCollectionConfigurer : IAppBuilderConfigurer
{
    protected readonly IServiceProvider _serviceProvider;

    public virtual int Order { get; set; } = 100;

    public ServiceCollectionConfigurer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public virtual async Task ConfigureAsync(WebApplicationBuilder builder)
        => await _serviceProvider.ServicesActionAsync<IServiceConfigurer>(async configurer => await configurer.ConfigureAsync(builder.Services));
}