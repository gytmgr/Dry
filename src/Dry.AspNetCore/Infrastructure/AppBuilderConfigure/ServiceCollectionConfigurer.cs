﻿namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 服务配置类
/// </summary>
public class ServiceCollectionConfigurer : IAppBuilderConfigurer, ISingletonDependency<IAppBuilderConfigurer>
{
    protected readonly IServiceProvider _serviceProvider;

    public int Order { get; set; } = 100;

    public ServiceCollectionConfigurer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public virtual async Task ConfigureAsync(WebApplicationBuilder builder)
        => await WebAppHelper.ServicesActionAsync<IServiceConfigurer>(_serviceProvider, async configurer => await configurer.ConfigureAsync(builder.Services));
}