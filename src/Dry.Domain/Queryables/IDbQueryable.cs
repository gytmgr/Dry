#nullable enable

namespace Dry.Domain.Queryables;

/// <summary>
/// 数据库查询接口
/// </summary>
public interface IDbQueryable
{
    /// <summary>
    /// 查询字符串
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    string ToQueryString(IQueryable queryable);

    #region 判断

    /// <summary>
    /// 是否有
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> AnyAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 是否全是
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<bool> AllAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate);

    /// <summary>
    /// 包含
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    Task<bool> ContainsAsync<TSource>(IQueryable<TSource> queryable, TSource item);

    #endregion

    #region 数量

    /// <summary>
    /// 数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<int> CountAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 长整型数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<long> LongCountAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    #endregion

    #region 数据

    /// <summary>
    /// 第一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource> FirstAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 第一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource?> FirstOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 最后一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource> LastAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 最后一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource?> LastOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 单条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource> SingleAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 单条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<TSource?> SingleOrDefaultAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null);

    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> queryable);

    /// <summary>
    /// 数组
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<TSource[]> ToArrayAsync<TSource>(IQueryable<TSource> queryable);

    /// <summary>
    /// 字典
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer = null) where TKey : notnull;

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
    Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer = null) where TKey : notnull;

    #endregion

    #region 计算

    #region 最小

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<TSource> MinAsync<TSource>(IQueryable<TSource> queryable);

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<TResult> MinAsync<TSource, TResult>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector);

    #endregion

    #region 最大

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<TSource> MaxAsync<TSource>(IQueryable<TSource> queryable);

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<TResult> MaxAsync<TSource, TResult>(IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector);

    #endregion

    #region 和

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<decimal> SumAsync(IQueryable<decimal> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<decimal?> SumAsync(IQueryable<decimal?> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<decimal> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<decimal?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<int> SumAsync(IQueryable<int> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<int?> SumAsync(IQueryable<int?> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<int> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<int?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<long> SumAsync(IQueryable<long> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<long?> SumAsync(IQueryable<long?> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<long> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<long?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double> SumAsync(IQueryable<double> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double?> SumAsync(IQueryable<double?> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<float> SumAsync(IQueryable<float> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<float?> SumAsync(IQueryable<float?> queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<float> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<float?> SumAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector);

    #endregion

    #region 平均

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<decimal> AverageAsync(IQueryable<decimal> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<decimal?> AverageAsync(IQueryable<decimal?> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<decimal> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<decimal?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double> AverageAsync(IQueryable<int> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double?> AverageAsync(IQueryable<int?> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double> AverageAsync(IQueryable<long> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double?> AverageAsync(IQueryable<long?> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double> AverageAsync(IQueryable<double> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<double?> AverageAsync(IQueryable<double?> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<double?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<float> AverageAsync(IQueryable<float> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task<float?> AverageAsync(IQueryable<float?> queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<float> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<float?> AverageAsync<TSource>(IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector);

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
    IQueryable<TEntity> Include<TEntity, TProperty>(IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class;

    /// <summary>
    /// 提前加载多属性实体的属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPreviousProperty"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="navigationPropertyPath"></param>
    /// <returns></returns>
    IQueryable<TEntity> ThenInclude<TEntity, TPreviousProperty, TProperty>(IQueryable<TEntity> queryable, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class;

    /// <summary>
    /// 忽略自动加载
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IQueryable<TEntity> IgnoreAutoIncludes<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

    /// <summary>
    /// 忽略查询筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IQueryable<TEntity> IgnoreQueryFilters<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

    /// <summary>
    /// 不跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IQueryable<TEntity> AsNoTracking<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

    /// <summary>
    /// 不跟踪但标识解析
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IQueryable<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

    /// <summary>
    /// 跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IQueryable<TEntity> AsTracking<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

    /// <summary>
    /// 标记
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    IQueryable<TSource> TagWith<TSource>(IQueryable<TSource> queryable, string tag);

    #endregion

    #region 加载

    /// <summary>
    /// 延迟加载
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    Task LoadAsync<TSource>(IQueryable<TSource> queryable);

    #endregion

    #region 遍历

    /// <summary>
    /// 遍历
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    Task ForEachAsync<TSource>(IQueryable<TSource> queryable, Action<TSource> action);

    /// <summary>
    /// 异步迭代器
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(IQueryable<TSource> queryable);

    #endregion
}