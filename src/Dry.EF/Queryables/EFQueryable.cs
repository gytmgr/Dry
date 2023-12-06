namespace Dry.EF.Queryables;

/// <summary>
/// EF查询
/// </summary>
public class EFQueryable : IDbQueryable
{
    /// <summary>
    /// 查询字符串
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual string ToQueryString(IQueryable queryable)
        => queryable.ToQueryString();

    #region 判断

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.AnyAsync() : await queryable.AnyAsync(predicate);

    /// <summary>
    /// 是否全是
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<bool> AllAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate)
        => await queryable.AllAsync(predicate);

    /// <summary>
    /// 包含
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public virtual async Task<bool> ContainsAsync<TSource>(IQueryable<TSource> queryable, TSource item)
        => await queryable.ContainsAsync(item);

    #endregion

    #region 数量

    /// <summary>
    /// 数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<int> CountAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.CountAsync() : await queryable.CountAsync(predicate);

    /// <summary>
    /// 长整型数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<long> LongCountAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.LongCountAsync() : await queryable.LongCountAsync(predicate);

    #endregion

    #region 数据

#if NET8_0_OR_GREATER

    /// <summary>
    /// 指定索引
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual async Task<TSource> ElementAtAsync<TSource>(IQueryable<TSource> queryable, int index)
        => await queryable.ElementAtAsync(index);

    /// <summary>
    /// 指定索引或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual async Task<TSource> ElementAtOrDefaultAsync<TSource>(IQueryable<TSource> queryable, int index)
        => await queryable.ElementAtOrDefaultAsync(index);

#endif

    /// <summary>
    /// 第一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource> FirstAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.FirstAsync() : await queryable.FirstAsync(predicate);

    /// <summary>
    /// 第一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource?> FirstOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.FirstOrDefaultAsync() : await queryable.FirstOrDefaultAsync(predicate);

    /// <summary>
    /// 最后一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource> LastAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.LastAsync() : await queryable.LastAsync(predicate);

    /// <summary>
    /// 最后一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource?> LastOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.LastOrDefaultAsync() : await queryable.LastOrDefaultAsync(predicate);

    /// <summary>
    /// 单条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource> SingleAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.SingleAsync() : await queryable.SingleAsync(predicate);

    /// <summary>
    /// 单条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<TSource?> SingleOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null)
        => predicate is null ? await queryable.SingleOrDefaultAsync() : await queryable.SingleOrDefaultAsync(predicate);

    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.ToListAsync();

    /// <summary>
    /// 数组
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<TSource[]> ToArrayAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.ToArrayAsync();

    /// <summary>
    /// 字典
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public virtual async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer = null) where TKey : notnull
        => comparer is null ? await queryable.ToDictionaryAsync(keySelector) : await queryable.ToDictionaryAsync(keySelector, comparer);

    /// <summary>
    /// 字典
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="keySelector"></param>
    /// <param name="elementSelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public virtual async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer = null) where TKey : notnull
        => comparer is null ? await queryable.ToDictionaryAsync(keySelector, elementSelector) : await queryable.ToDictionaryAsync(keySelector, elementSelector, comparer);

    #endregion

    #region 计算

    #region 最小

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<TSource> MinAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.MinAsync();

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<TResult> MinAsync<TSource, TResult>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector)
        => await queryable.MinAsync(selector);

    #endregion

    #region 最大

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<TSource> MaxAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.MaxAsync();

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<TResult> MaxAsync<TSource, TResult>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector)
        => await queryable.MaxAsync(selector);

    #endregion

    #region 和

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<decimal> SumAsync(IQueryable<decimal> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> SumAsync(IQueryable<decimal?> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<decimal> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<int> SumAsync(IQueryable<int> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<int?> SumAsync(IQueryable<int?> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<int> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<int?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<long> SumAsync(IQueryable<long> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<long?> SumAsync(IQueryable<long?> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<long> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<long?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double> SumAsync(IQueryable<double> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double?> SumAsync(IQueryable<double?> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<float> SumAsync(IQueryable<float> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<float?> SumAsync(IQueryable<float?> queryable)
        => await queryable.SumAsync();

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<float> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector)
        => await queryable.SumAsync(selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<float?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector)
        => await queryable.SumAsync(selector);

    #endregion

    #region 平均

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<decimal> AverageAsync(IQueryable<decimal> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> AverageAsync(IQueryable<decimal?> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<decimal> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<decimal?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync(IQueryable<int> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync(IQueryable<int?> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync(IQueryable<long> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync(IQueryable<long?> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync(IQueryable<double> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync(IQueryable<double?> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<float> AverageAsync(IQueryable<float> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<float?> AverageAsync(IQueryable<float?> queryable)
        => await queryable.AverageAsync();

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<float> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector)
        => await queryable.AverageAsync(selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public virtual async Task<float?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector)
        => await queryable.AverageAsync(selector);

    #endregion

    #endregion

    #region 查询设置

    /// <summary>
    /// 提前加载
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="navigationPropertyPath"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> Include<TEntity, TProperty>(IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        => queryable.Include(navigationPropertyPath);

    /// <summary>
    /// 提前加载多属性实体的属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPreviousProperty"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="navigationPropertyPath"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> ThenInclude<TEntity, TPreviousProperty, TProperty>(IQueryable<TEntity> queryable, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        => queryable switch
        {
            IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> includableQueryableArray => includableQueryableArray.ThenInclude(navigationPropertyPath),
            IIncludableQueryable<TEntity, TPreviousProperty> includableQueryableSingle => includableQueryableSingle.ThenInclude(navigationPropertyPath),
            _ => queryable
        };

    /// <summary>
    /// 忽略自动加载
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> IgnoreAutoIncludes<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.IgnoreAutoIncludes();

    /// <summary>
    /// 忽略查询筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> IgnoreQueryFilters<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.IgnoreQueryFilters();

    /// <summary>
    /// 不跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> AsNoTracking<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.AsNoTracking();

    /// <summary>
    /// 不跟踪但标识解析
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.AsNoTrackingWithIdentityResolution();

    /// <summary>
    /// 跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> AsTracking<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.AsTracking();

    /// <summary>
    /// 标记
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public virtual IQueryable<TSource> TagWith<TSource>(IQueryable<TSource> queryable, string tag)
        => queryable.TagWith(tag);

    /// <summary>
    /// 单个查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> AsSingleQuery<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.AsSingleQuery();

    /// <summary>
    /// 拆分查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IQueryable<TEntity> AsSplitQuery<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        => queryable.AsSplitQuery();

    #endregion

    #region 加载

    /// <summary>
    /// 延迟加载
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task LoadAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.LoadAsync();

    #endregion

    #region 遍历

    /// <summary>
    /// 遍历
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public virtual async Task ForEachAsync<TSource>(IQueryable<TSource> queryable, Action<TSource> action)
        => await queryable.ForEachAsync(action);

    /// <summary>
    /// 异步迭代器
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(IQueryable<TSource> queryable)
        => queryable.AsAsyncEnumerable();

    #endregion

#if NET8_0_OR_GREATER


    #region 删除

    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public virtual async Task<int> ExecuteDeleteAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.ExecuteDeleteAsync();

    #endregion

#endif

}