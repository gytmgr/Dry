namespace Dry.EF8.Repositories;

/// <summary>
/// ef8只读仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EF8ReadOnlyRepository<TEntity> : ReadOnlyRepository<TEntity> where TEntity : class, IEntity, IBoundedContext
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public EF8ReadOnlyRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// 获取查询
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override IQueryable<TEntity> GetQueryableFromSql(FormattableString sql)
        => _context.Set<TEntity>().FromSql(sql).AsNoTracking();

#endif
}