global using Dry.Domain.Shared;
global using Dry.EF.Contexts;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace Dry.EF.SqlServer;

/// <summary>
/// sql server ef上下文配置器
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class SqlServerDbContextConfigurer<TBoundedContext> : DryDbContextConfigurer<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 数据库字段名称
    /// </summary>
    protected override string DbFieldName { get; } = "initial catalog";

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="optionsBuilder"></param>
    public override void Configuring(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = GetTenantConnectionString(serviceProvider);
        optionsBuilder.UseSqlServer(tenantConnectionString!, x => DvContextOptionsBuilderConfiguring<SqlServerDbContextOptionsBuilder, SqlServerOptionsExtension>(x));
    }
}