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
public abstract class SqlServerDbContextConfigurerBase<TBoundedContext> : DryDbContextConfigurerBase<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 数据库字段名称
    /// </summary>
    protected override string DbFieldName { get; } = "initial catalog";

    /// <summary>
    /// 配置数据库
    /// </summary>
    /// <param name="tenantConnectionString"></param>
    /// <param name="optionsBuilder"></param>
    protected override void UseDb(string tenantConnectionString, DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(tenantConnectionString!, x => DvContextOptionsBuilderConfiguring<SqlServerDbContextOptionsBuilder, SqlServerOptionsExtension>(x));
}