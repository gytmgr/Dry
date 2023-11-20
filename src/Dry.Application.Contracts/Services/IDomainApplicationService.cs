namespace Dry.Application.Contracts.Services;

/// <summary>
/// 领域应用服务接口
/// </summary>
public interface IDomainApplicationService<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 数据库连接字符串配置
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    Task DbConnectionStringSetAsync(string connectionString);

    /// <summary>
    /// 数据库迁移
    /// </summary>
    /// <returns></returns>
    Task DbMigrateAsync();
}