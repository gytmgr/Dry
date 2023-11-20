namespace Dry.Domain.Contexts;

/// <summary>
/// 数据库上下文接口
/// </summary>
public interface IDryDbContext
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    string? ConnectionString { get; set; }

    /// <summary>
    /// 自动事务是否启用
    /// </summary>
    bool AutoTransactionsEnabled { get; set; }

    /// <summary>
    /// 自动创建保存点是否启用
    /// </summary>
    bool AutoSavepointsEnabled { get; set; }

    /// <summary>
    /// 自动检测更改是否启用
    /// </summary>
    bool AutoDetectChangesEnabled { get; set; }

    /// <summary>
    /// 延迟加载是否启用
    /// </summary>
    bool LazyLoadingEnabled { get; set; }

    /// <summary>
    /// 查询跟踪是否启用
    /// </summary>
    bool QueryTrackingEnabled { get; set; }

    /// <summary>
    /// 是否有改动
    /// </summary>
    bool HasChanges { get; }

    /// <summary>
    /// 迁移
    /// </summary>
    /// <returns></returns>
    Task MigrateAsync();

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <returns></returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// 提交事务
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <returns></returns>
    Task RollbackTransactionAsync();

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql);

    /// <summary>
    /// 清除所有实体跟踪
    /// </summary>
    void TrackingClear();
}

/// <summary>
/// 数据库上下文接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public interface IDryDbContext<TBoundedContext> : IDryDbContext where TBoundedContext : IBoundedContext
{
}