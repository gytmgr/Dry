#nullable enable

namespace Dry.Domain.Extensions;

/// <summary>
/// Queryable扩展
/// </summary>
public static class QueryableExtension
{
    private static IDbQueryable GetDbQueryable(IDbQueryable? dbQueryable)
        => dbQueryable is null ? IDependency.RootServiceProvider.GetService<IDbQueryable>()! : dbQueryable;

    #region 判断

    /// <summary>
    /// 是否有
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<bool> AnyAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AnyAsync(queryable, predicate);

    /// <summary>
    /// 是否全是
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<bool> AllAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>> predicate, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AllAsync(queryable, predicate);

    /// <summary>
    /// 包含
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="item"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> queryable, TSource item, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).ContainsAsync(queryable, item);

    #endregion

    #region 数量

    /// <summary>
    /// 数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<int> CountAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).CountAsync(queryable, predicate);

    /// <summary>
    /// 长整型数量
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<long> LongCountAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).LongCountAsync(queryable, predicate);

    #endregion

    #region 数据

    /// <summary>
    /// 第一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).FirstAsync(queryable, predicate);

    /// <summary>
    /// 第一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource?> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).FirstOrDefaultAsync(queryable, predicate);

    /// <summary>
    /// 最后一条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource> LastAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).LastAsync(queryable, predicate);

    /// <summary>
    /// 最后一条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource?> LastOrDefaultAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).LastOrDefaultAsync(queryable, predicate);

    /// <summary>
    /// 单条
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SingleAsync(queryable, predicate);

    /// <summary>
    /// 单条或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="predicate"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource?> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, bool>>? predicate = null, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SingleAsync(queryable, predicate);

    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).ToListAsync(queryable);

    /// <summary>
    /// 数组
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).ToArrayAsync(queryable);

    /// <summary>
    /// 字典
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="keySelector"></param>
    /// <param name="comparer"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer = null, IDbQueryable? dbQueryable = null) where TKey : notnull
        => await GetDbQueryable(dbQueryable).ToDictionaryAsync(queryable, keySelector, comparer);

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
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> queryable, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer = null, IDbQueryable? dbQueryable = null) where TKey : notnull
        => await GetDbQueryable(dbQueryable).ToDictionaryAsync(queryable, keySelector, elementSelector, comparer);

    #endregion

    #region 计算

    #region 最小

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource> MinAsync<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).MinAsync(queryable);

    /// <summary>
    /// 最小
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).MinAsync(queryable, selector);

    #endregion

    #region 最大

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).MaxAsync(queryable);

    /// <summary>
    /// 最大
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> queryable, Expression<Func<TSource, TResult>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).MaxAsync(queryable, selector);

    #endregion

    #region 和

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal> SumAsync(this IQueryable<decimal> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal?> SumAsync(this IQueryable<decimal?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<int> SumAsync(this IQueryable<int> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<int?> SumAsync(this IQueryable<int?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<int> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<int?> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<long> SumAsync(this IQueryable<long> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<long?> SumAsync(this IQueryable<long?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<long> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<long?> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> SumAsync(this IQueryable<double> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> SumAsync(this IQueryable<double?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float> SumAsync(this IQueryable<float> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float?> SumAsync(this IQueryable<float?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    /// <summary>
    /// 和
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float?> SumAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).SumAsync(queryable, selector);

    #endregion

    #region 平均

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal> AverageAsync(this IQueryable<decimal> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal?> AverageAsync(this IQueryable<decimal?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, decimal>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, decimal?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync(this IQueryable<int> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync(this IQueryable<int?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, int>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, int?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync(this IQueryable<long> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync(this IQueryable<long?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, long>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, long?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync(this IQueryable<double> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync(this IQueryable<double?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, double>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<double?> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, double?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float> AverageAsync(this IQueryable<float> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float?> AverageAsync(this IQueryable<float?> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, float>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

    /// <summary>
    /// 平均
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="selector"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task<float?> AverageAsync<TSource>(this IQueryable<TSource> queryable, Expression<Func<TSource, float?>> selector, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).AverageAsync(queryable, selector);

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
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>> navigationPropertyPath, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).Include(queryable, navigationPropertyPath);

    /// <summary>
    /// 提前加载
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> Include<TEntity, TProperty>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, TProperty>>[] navigationPropertyPaths, IDbQueryable? dbQueryable = null) where TEntity : class
    {
        if (navigationPropertyPaths is not null)
        {
            dbQueryable = GetDbQueryable(dbQueryable);
            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                queryable = dbQueryable.Include(queryable, navigationPropertyPath);
            }
        }
        return queryable;
    }

    /// <summary>
    /// 提前加载多属性实体的属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPreviousProperty"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="navigationPropertyPath"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> ThenInclude<TEntity, TPreviousProperty, TProperty>(this IQueryable<TEntity> queryable, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).ThenInclude(queryable, navigationPropertyPath);

    /// <summary>
    /// 忽略自动加载
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> IgnoreAutoIncludes<TEntity>(this IQueryable<TEntity> queryable, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).IgnoreAutoIncludes(queryable);

    /// <summary>
    /// 忽略查询筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> IgnoreQueryFilters<TEntity>(this IQueryable<TEntity> queryable, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).IgnoreQueryFilters(queryable);

    /// <summary>
    /// 不跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> queryable, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).AsNoTracking(queryable);

    /// <summary>
    /// 不跟踪但标识解析
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> AsNoTrackingWithIdentityResolution<TEntity>(this IQueryable<TEntity> queryable, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).AsNoTrackingWithIdentityResolution(queryable);

    /// <summary>
    /// 跟踪
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> AsTracking<TEntity>(this IQueryable<TEntity> queryable, IDbQueryable? dbQueryable = null) where TEntity : class
        => GetDbQueryable(dbQueryable).AsTracking(queryable);

    /// <summary>
    /// 标记
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="tag"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IQueryable<TSource> TagWith<TSource>(this IQueryable<TSource> queryable, string tag, IDbQueryable? dbQueryable = null)
        => GetDbQueryable(dbQueryable).TagWith(queryable, tag);

    #endregion

    #region 加载

    /// <summary>
    /// 延迟加载
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task LoadAsync<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).LoadAsync(queryable);

    #endregion

    #region 遍历

    /// <summary>
    /// 遍历
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="action"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static async Task ForEachAsync<TSource>(this IQueryable<TSource> queryable, Action<TSource> action, IDbQueryable? dbQueryable = null)
        => await GetDbQueryable(dbQueryable).ForEachAsync(queryable, action);

    /// <summary>
    /// 异步迭代器
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="dbQueryable"></param>
    /// <returns></returns>
    public static IAsyncEnumerable<TSource> AsAsyncEnumerable<TSource>(this IQueryable<TSource> queryable, IDbQueryable? dbQueryable = null)
        => GetDbQueryable(dbQueryable).AsAsyncEnumerable(queryable);

    #endregion
}