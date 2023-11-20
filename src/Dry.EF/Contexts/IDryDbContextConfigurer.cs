namespace Dry.EF.Contexts;

/// <summary>
/// ef上下文配置器接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public interface IDryDbContextConfigurer<TBoundedContext> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    string? ConnectionString { get; set; }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="optionsBuilder"></param>
    void Configuring(IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder);
}