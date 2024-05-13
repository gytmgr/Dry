namespace Dry.EF.Contexts;

/// <summary>
/// ef上下文
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public class DryDbContext<TBoundedContext> : DbContext, IDryDbContext<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 连接字符串
    /// </summary>
    public virtual string? ConnectionString
    {
        get => _serviceProvider.GetRequiredService<IDryDbContextConfigurer<TBoundedContext>>().ConnectionString;
        set => _serviceProvider.GetRequiredService<IDryDbContextConfigurer<TBoundedContext>>().ConnectionString = value;
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// 自动事务是否启用
    /// </summary>
    public virtual bool AutoTransactionsEnabled
    {
        get => Database.AutoTransactionBehavior is not AutoTransactionBehavior.Never;
        set => Database.AutoTransactionBehavior = value ? AutoTransactionBehavior.WhenNeeded : AutoTransactionBehavior.Never;
    }

#else

    /// <summary>
    /// 自动事务是否启用
    /// </summary>
    public virtual bool AutoTransactionsEnabled
    {
        get => Database.AutoTransactionsEnabled;
        set => Database.AutoTransactionsEnabled = value;
    }

#endif

    /// <summary>
    /// 自动创建保存点是否启用
    /// </summary>
    public virtual bool AutoSavepointsEnabled
    {
        get => Database.AutoSavepointsEnabled;
        set => Database.AutoSavepointsEnabled = value;
    }

    /// <summary>
    /// 自动检测更改是否启用
    /// </summary>
    public virtual bool AutoDetectChangesEnabled
    {
        get => ChangeTracker.AutoDetectChangesEnabled;
        set => ChangeTracker.AutoDetectChangesEnabled = value;
    }

    /// <summary>
    /// 延迟加载是否启用
    /// </summary>
    public virtual bool LazyLoadingEnabled
    {
        get => ChangeTracker.LazyLoadingEnabled;
        set => ChangeTracker.LazyLoadingEnabled = value;
    }

    /// <summary>
    /// 查询跟踪是否启用
    /// </summary>
    public virtual bool QueryTrackingEnabled
    {
        get => ChangeTracker.QueryTrackingBehavior switch
        {
            QueryTrackingBehavior.TrackAll => true,
            _ => false
        };
        set => ChangeTracker.QueryTrackingBehavior = value switch
        {
            true => QueryTrackingBehavior.TrackAll,
            _ => QueryTrackingBehavior.NoTrackingWithIdentityResolution
        };
    }

    /// <summary>
    /// 是否有改动
    /// </summary>
    public virtual bool HasChanges
    {
        get => ChangeTracker.HasChanges();
    }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public DryDbContext(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 选项配置
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _serviceProvider.GetRequiredService<IDryDbContextConfigurer<TBoundedContext>>().Configuring(_serviceProvider, optionsBuilder);
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// 注册实体配置
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityRegisters = _serviceProvider.GetServices<IEntityRegister<TBoundedContext>>();
        foreach (var entityRegister in entityRegisters)
        {
            entityRegister.RegistTo(modelBuilder);
        }
        base.OnModelCreating(modelBuilder);

#if NET6_0

        modelBuilder.ConfigDatabaseDescription();

#endif

        _serviceProvider.GetRequiredService<IDryDbContextConfigurer<TBoundedContext>>().OnModelCreated(modelBuilder);
    }

    /// <summary>
    /// 迁移
    /// </summary>
    /// <returns></returns>
    public virtual async Task MigrateAsync()
    {
        var pendingMigrations = await Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await Database.MigrateAsync();
        }
    }

    /// <summary>
    /// 开始事务
    /// </summary>
    /// <returns></returns>
    public virtual async Task BeginTransactionAsync()
        => await Database.BeginTransactionAsync();

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <returns></returns>
    public virtual async Task CommitTransactionAsync()
        => await Database.CommitTransactionAsync();

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <returns></returns>
    public virtual async Task RollbackTransactionAsync()
        => await Database.RollbackTransactionAsync();

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public virtual async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        => await Database.ExecuteSqlRawAsync(sql, parameters);

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual async Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql)
        => await Database.ExecuteSqlInterpolatedAsync(sql);

    /// <summary>
    /// 清除所有实体跟踪
    /// </summary>
    public virtual void TrackingClear()
        => ChangeTracker.Clear();
}