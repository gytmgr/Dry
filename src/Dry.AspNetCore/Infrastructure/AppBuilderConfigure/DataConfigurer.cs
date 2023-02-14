namespace Dry.AspNetCore.Infrastructure.AppBuilderConfigure;

/// <summary>
/// 数据配置类
/// </summary>
public class DataConfigurer : IAppBuilderConfigurer, ISingletonDependency<IAppBuilderConfigurer>
{
    protected readonly IServiceProvider _serviceProvider;

    public int Order { get; set; } = default;

    public DataConfigurer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public virtual async Task ConfigureAsync(WebApplicationBuilder builder)
        => await WebAppHelper.ServicesActionAsync<IDataIniter>(_serviceProvider, async initer => await initer.InitAsync());
}