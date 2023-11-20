global using Dry.Domain.Shared;
global using Dry.EF.Contexts;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;

namespace Dry.EF.Sqlite;

/// <summary>
/// sqlite ef上下文配置器
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class SqliteDbContextConfigurer<TBoundedContext> : DryDbContextConfigurer<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 数据库字段名称
    /// </summary>
    protected override string DbFieldName { get; } = "data source";

    /// <summary>
    /// 获取租户连接字符串数据库片段
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="connectionStringDbSpan"></param>
    /// <returns></returns>
    protected override string GetTenantConnectionStringDbSpan(string tenantId, string connectionStringDbSpan)
    {
        var dbName = connectionStringDbSpan[(DbFieldName.Length + 1)..^4];
        return $"{DbFieldName}={dbName}_{tenantId}.db";
    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="optionsBuilder"></param>
    public override void Configuring(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = GetTenantConnectionString(serviceProvider);
        optionsBuilder.UseSqlite(tenantConnectionString!, x => DvContextOptionsBuilderConfiguring<SqliteDbContextOptionsBuilder, SqliteOptionsExtension>(x));
    }
}