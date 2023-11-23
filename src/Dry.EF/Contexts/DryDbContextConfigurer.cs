namespace Dry.EF.Contexts;

/// <summary>
/// ef上下文配置器
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class DryDbContextConfigurer<TBoundedContext> : IDryDbContextConfigurer<TBoundedContext>, ISingletonDependency<IDryDbContextConfigurer<TBoundedContext>> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 数据库字段名称
    /// </summary>
    protected abstract string DbFieldName { get; }

    /// <summary>
    /// 全局拆分查询配置
    /// </summary>
    protected virtual QuerySplittingBehavior? QuerySplittingBehavior { get; }

    /// <summary>
    /// 连接字符串
    /// </summary>
    public virtual string? ConnectionString { get; set; }

    /// <summary>
    /// 获取租户连接字符串数据库片段
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="connectionStringDbSpan"></param>
    /// <returns></returns>
    protected virtual string GetTenantConnectionStringDbSpan(string tenantId, string connectionStringDbSpan)
    {
        var dbName = connectionStringDbSpan[(DbFieldName.Length + 1)..];
        return $"{DbFieldName}={dbName}_{tenantId}";
    }

    /// <summary>
    /// 获取租户连接字符串
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    protected virtual string? GetTenantConnectionString(IServiceProvider serviceProvider)
    {
        var tenantId = serviceProvider.GetRequiredService<ITenantProvider>().Id;
        if (tenantId is not null && ConnectionString is not null)
        {
            var connetionStringSpans = ConnectionString.Split(';');
            var connectionStringDbSpanInfo = connetionStringSpans
                .Select((x, index) => new { Index = index, ConnectionStringDbSpan = x })
                .Where(x => x.ConnectionStringDbSpan.StartsWith($"{DbFieldName}=", StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
            if (connectionStringDbSpanInfo is not null)
            {
                connetionStringSpans[connectionStringDbSpanInfo.Index] = GetTenantConnectionStringDbSpan(tenantId, connectionStringDbSpanInfo.ConnectionStringDbSpan);
            }
            return string.Join(";", connetionStringSpans);
        }
        return ConnectionString;
    }

    /// <summary>
    /// 上下文选项配置
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    /// <typeparam name="TExtension"></typeparam>
    /// <param name="optionsBuilder"></param>
    protected virtual void DvContextOptionsBuilderConfiguring<TBuilder, TExtension>(TBuilder optionsBuilder)
        where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
        where TExtension : RelationalOptionsExtension, new()
    {
        optionsBuilder.CommandTimeout(120);
        optionsBuilder.MigrationsAssembly(GetType().Assembly.GetName().Name);
        if (QuerySplittingBehavior.HasValue)
        {
            optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.Value);
        }
    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="optionsBuilder"></param>
    public abstract void Configuring(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder);
}