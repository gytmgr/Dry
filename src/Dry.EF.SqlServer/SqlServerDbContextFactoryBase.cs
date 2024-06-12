namespace Dry.EF.SqlServer;

/// <summary>
/// sql server ef上下文工厂（生产迁移脚本用）
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public abstract class SqlServerDbContextFactoryBase<TBoundedContext> : DbContextFactoryBase<EF8DryDbContext<TBoundedContext>, TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    protected override string ConnectionString => "Server=(localdb)\\MSSQLLocalDB;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
}