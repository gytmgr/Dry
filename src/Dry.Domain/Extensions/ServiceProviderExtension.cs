namespace Dry.Domain.Extensions;

/// <summary>
/// 服务创建扩展
/// </summary>
public static class ServiceProviderExtension
{
    /// <summary>
    /// 获取只读仓储服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IReadOnlyRepository<TEntity> GetReadOnlyRepository<TEntity>(this IServiceProvider serviceProvider) where TEntity : class, IEntity, IBoundedContext
        => serviceProvider.GetRequiredService<IReadOnlyRepository<TEntity>>();

    /// <summary>
    /// 获取仓储服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IRepository<TEntity> GetRepository<TEntity>(this IServiceProvider serviceProvider) where TEntity : class, IEntity, IBoundedContext
        => serviceProvider.GetRequiredService<IRepository<TEntity>>();

    /// <summary>
    /// 获取数据库上下文
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IDryDbContext GetDryDbContext<TEntity>(this IServiceProvider serviceProvider) where TEntity : class, IEntity, IBoundedContext
    {
        var interfaces = typeof(TEntity).GetInterfaces();
        var boundedContextType = interfaces.First(x => x != typeof(IBoundedContext) && typeof(IBoundedContext).IsAssignableFrom(x));
        var dbContextType = typeof(IDryDbContext<>).MakeGenericType(boundedContextType);
        return (IDryDbContext)serviceProvider.GetRequiredService(dbContextType);
    }

    /// <summary>
    /// 获取工作单元
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IUnitOfWork GetUnitOfWork<TEntity>(this IServiceProvider serviceProvider) where TEntity : class, IEntity, IBoundedContext
    {
        var interfaces = typeof(TEntity).GetInterfaces();
        var boundedContextType = interfaces.First(x => x != typeof(IBoundedContext) && typeof(IBoundedContext).IsAssignableFrom(x));
        var dbContextType = typeof(IUnitOfWork<>).MakeGenericType(boundedContextType);
        return (IUnitOfWork)serviceProvider.GetRequiredService(dbContextType);
    }
}