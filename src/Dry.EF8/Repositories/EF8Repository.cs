namespace Dry.EF8.Repositories;

/// <summary>
/// ef8仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EF8Repository<TEntity> : Repository<TEntity> where TEntity : class, IEntity, IBoundedContext
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="provider"></param>
    public EF8Repository(IServiceProvider provider) : base(provider)
    {
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// 获取查询
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override IQueryable<TEntity> GetQueryableFromSql(FormattableString sql)
        => _context.Set<TEntity>().FromSql(sql);

#endif
}