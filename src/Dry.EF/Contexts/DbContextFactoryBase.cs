namespace Dry.EF.Contexts;

/// <summary>
/// ef上下文工厂（生产迁移脚本用）
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class DbContextFactoryBase<TDbContext, TBoundedContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DryDbContext<TBoundedContext>
    where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 注入程序集命名前缀
    /// </summary>
    protected virtual string[]? DependencyPrefixs { get; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    protected abstract string ConnectionString { get; }

    /// <summary>
    /// 创建上下文
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual TDbContext CreateDbContext(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDependency(true, DependencyPrefixs)
            .BuildServiceProvider();
        serviceProvider.GetRequiredService<IDryDbContextConfigurer<TBoundedContext>>().ConnectionString = ConnectionString;
        return (TDbContext)serviceProvider.GetRequiredService<IDryDbContext<TBoundedContext>>();
    }
}