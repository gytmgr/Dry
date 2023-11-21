namespace Dry.Application.Services;

/// <summary>
/// 领域应用服务
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public class DomainApplicationService<TBoundedContext> : IDomainApplicationService<TBoundedContext>, IDependency<IDomainApplicationService<TBoundedContext>> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 服务生成器
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public DomainApplicationService(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 数据库连接字符串配置
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public virtual Task DbConnectionStringSetAsync(string connectionString)
    {
        _serviceProvider.GetRequiredService<IDryDbContext<TBoundedContext>>().ConnectionString = connectionString;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 数据库迁移
    /// </summary>
    /// <returns></returns>
    public virtual async Task DbMigrateAsync()
        => await _serviceProvider.GetRequiredService<IDryDbContext<TBoundedContext>>().MigrateAsync();
}