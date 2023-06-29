namespace Dry.Core.Utilities;

/// <summary>
/// Enumerable扩展
/// </summary>
public static class EnumerableExtension
{
    /// <summary>
    /// 多字段排序
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector)[] orderBys)
    {
        if (orderBys != null && orderBys.Length > 0)
        {
            var orderQuery = default(IOrderedEnumerable<TSource>);
            foreach (var (isAsc, keySelector) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderBy(keySelector);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenBy(keySelector);
                    }
                }
                else
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderByDescending(keySelector);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector);
                    }
                }
            }
            query = orderQuery;
        }
        return query;
    }

    /// <summary>
    /// 使用比较器多字段排序
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector, IComparer<dynamic> comparer)[] orderBys)
    {
        if (orderBys != null && orderBys.Length > 0)
        {
            var orderQuery = default(IOrderedEnumerable<TSource>);
            foreach (var (isAsc, keySelector, comparer) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderBy(keySelector, comparer);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenBy(keySelector, comparer);
                    }
                }
                else
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderByDescending(keySelector, comparer);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector, comparer);
                    }
                }
            }
            query = orderQuery;
        }
        return query;
    }

#if NET5_0 || NETCOREAPP3_1

    /// <summary>
    /// 指定属性去重
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="query"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> query, Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (TSource element in query)
        {
            if (seenKeys.Add(keySelector(element)))
            {
                yield return element;
            }
        }
    }

#endif

    /// <summary>
    /// 差集
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="equalFunc"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TSource, bool> equalFunc)
        => first.Except(second, new CustomEqualityComparer<TSource>(equalFunc));
}

/// <summary>
/// Queryable扩展
/// </summary>
public static class QueryableExtension
{
    /// <summary>
    /// 设置多条件
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, bool>>[] predicates)
    {
        if (predicates is not null)
        {
            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }
        }
        return query;
    }

    /// <summary>
    /// 多字段排序
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)[] orderBys)
    {
        if (orderBys != null && orderBys.Length > 0)
        {
            var orderQuery = default(IOrderedQueryable<TSource>);
            foreach (var (isAsc, keySelector) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderBy(keySelector);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenBy(keySelector);
                    }
                }
                else
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderByDescending(keySelector);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector);
                    }
                }
            }
            query = orderQuery;
        }
        return query;
    }

    /// <summary>
    /// 使用比较器多字段排序
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector, IComparer<dynamic> comparer)[] orderBys)
    {
        if (orderBys != null && orderBys.Length > 0)
        {
            var orderQuery = default(IOrderedQueryable<TSource>);
            foreach (var (isAsc, keySelector, comparer) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderBy(keySelector, comparer);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenBy(keySelector, comparer);
                    }
                }
                else
                {
                    if (orderQuery == null)
                    {
                        orderQuery = query.OrderByDescending(keySelector, comparer);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector, comparer);
                    }
                }
            }
            query = orderQuery;
        }
        return query;
    }
}