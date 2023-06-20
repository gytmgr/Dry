namespace Dry.EF.Repositories;

/// <summary>
/// ef只读仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity, IBoundedContext
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    protected readonly IServiceProvider _provider;

    /// <summary>
    /// ef上下文
    /// </summary>
    protected readonly DbContext _context;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="provider"></param>
    public ReadOnlyRepository(IServiceProvider provider)
    {
        _provider = provider;
        var interfaces = typeof(TEntity).GetInterfaces();
        var boundedContextType = interfaces.First(x => x != typeof(IBoundedContext) && typeof(IBoundedContext).IsAssignableFrom(x));
        _context = (DbContext)_provider.GetService(boundedContextType);
    }

    /// <summary>
    /// 获取查询
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<TEntity> GetQueryable()
        => _context.Set<TEntity>().AsNoTracking();

    #region Bool

    /// <summary>
    /// 是否所有记录都满足条件
    /// </summary>
    /// <param name="allPredicate"></param>
    /// <param name="wherePredicates"></param>
    /// <returns></returns>
    public virtual async Task<bool> AllAsync([NotNull] Expression<Func<TEntity, bool>> allPredicate, params Expression<Func<TEntity, bool>>[] wherePredicates)
        => await GetQueryable().Where(wherePredicates).AllAsync(allPredicate);

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync(params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AnyAsync();

    #endregion

    #region Count

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<int> CountAsync(params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).CountAsync();

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<long> LongCountAsync(params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).LongCountAsync();

    #endregion

    #region QueryEntity

    /// <summary>
    /// 条件查询第一条并排序提前加载导航属性
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
    {
        var queryable = GetQueryable();
        if (paths is not null)
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
        }
        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }
        return await queryable.OrderBy(orderBys).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 排序条件查询第一条指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public virtual async Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
    {
        var queryable = GetQueryable();
        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }
        return await queryable.OrderBy(orderBys).Select(selector).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 条件查询并排序提前加载导航属性
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public virtual async Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
    {
        var queryable = GetQueryable();
        if (paths is not null)
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
        }
        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }
        return await queryable.OrderBy(orderBys).ToArrayAsync();
    }

    /// <summary>
    /// 排序条件查询指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
    {
        var queryable = GetQueryable();
        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }
        return await queryable.OrderBy(orderBys).Select(selector).ToArrayAsync();
    }

    /// <summary>
    /// 排序条件查询指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public virtual async Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys)
    {
        var queryable = GetQueryable();
        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }
        return await queryable.OrderBy(orderBys).SelectMany(selector).ToArrayAsync();
    }

    #endregion

    #region Sum

    /// <summary>
    /// 汇总
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<int?> SumAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).SumAsync(selector);

    /// <summary>
    /// 汇总
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<long?> SumAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).SumAsync(selector);

    /// <summary>
    /// 汇总
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<float?> SumAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).SumAsync(selector);

    /// <summary>
    /// 汇总
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<double?> SumAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).SumAsync(selector);

    /// <summary>
    /// 汇总
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> SumAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).SumAsync(selector);

    #endregion

    #region Max

    /// <summary>
    /// 最大值
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<TResult> MaxAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).MaxAsync(selector);

    #endregion

    #region Min

    /// <summary>
    /// 最小值
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<TResult> MinAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).MinAsync(selector);

    #endregion

    #region Average

    /// <summary>
    /// 平均值
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, int?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AverageAsync(selector);

    /// <summary>
    /// 平均值
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, long?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AverageAsync(selector);

    /// <summary>
    /// 平均值
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<float?> AverageAsync([NotNull] Expression<Func<TEntity, float?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AverageAsync(selector);

    /// <summary>
    /// 平均值
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync([NotNull] Expression<Func<TEntity, double?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AverageAsync(selector);

    /// <summary>
    /// 平均值
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> AverageAsync([NotNull] Expression<Func<TEntity, decimal?>> selector, params Expression<Func<TEntity, bool>>[] predicates)
        => await GetQueryable().Where(predicates).AverageAsync(selector);

    #endregion
}