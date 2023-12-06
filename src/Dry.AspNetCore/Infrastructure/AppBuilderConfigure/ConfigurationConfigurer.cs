namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// Configuration配置类
/// </summary>
public class ConfigurationConfigurer : IAppBuilderConfigurer
{
    protected readonly IServiceProvider _serviceProvider;

    public virtual int Order { get; set; } = int.MinValue;

    public ConfigurationConfigurer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public virtual async Task ConfigureAsync(WebApplicationBuilder builder)
        => await _serviceProvider.ServicesActionAsync<IConfigurationReader>(async reader => await reader.ReadAsync(builder.Configuration));
}