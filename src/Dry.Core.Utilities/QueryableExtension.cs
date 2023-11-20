namespace Dry.Core.Utilities;

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
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> query, Expression<Func<TSource, bool>>[]? predicates)
    {
        query.CheckParamNull(nameof(query));

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
    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)[]? orderBys)
    {
        query.CheckParamNull(nameof(query));

        if (orderBys?.Length > 0)
        {
            var orderQuery = default(IOrderedQueryable<TSource>);
            foreach (var (isAsc, keySelector) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery is null)
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
                    if (orderQuery is null)
                    {
                        orderQuery = query.OrderByDescending(keySelector);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector);
                    }
                }
            }
            query = orderQuery!;
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
    public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, params (bool isAsc, Expression<Func<TSource, dynamic>> keySelector, IComparer<dynamic> comparer)[]? orderBys)
    {
        query.CheckParamNull(nameof(query));

        if (orderBys?.Length > 0)
        {
            var orderQuery = default(IOrderedQueryable<TSource>);
            foreach (var (isAsc, keySelector, comparer) in orderBys)
            {
                if (isAsc)
                {
                    if (orderQuery is null)
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
                    if (orderQuery is null)
                    {
                        orderQuery = query.OrderByDescending(keySelector, comparer);
                    }
                    else
                    {
                        orderQuery = orderQuery.ThenByDescending(keySelector, comparer);
                    }
                }
            }
            query = orderQuery!;
        }
        return query;
    }
}