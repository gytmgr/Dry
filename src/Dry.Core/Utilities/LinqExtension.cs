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

/// <summary>
/// Lambda表达式扩展
/// </summary>
public static class ExpressionExtension
{
    /// <summary>
    /// 条件合并_并集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression1"></param>
    /// <param name="expression2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        if (expression1 is null)
        {
            return expression2;
        }
        if (expression2 is null)
        {
            return expression1;
        }
        var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
    }

    /// <summary>
    /// 条件合并_交集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression1"></param>
    /// <param name="expression2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        if (expression1 is null)
        {
            return expression2;
        }
        if (expression2 is null)
        {
            return expression1;
        }
        var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
    }
}

/// <summary>
/// linq帮助类
/// </summary>
public static class LinqHelper
{
    /// <summary>
    /// 获取表达式
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="keyName"></param>
    /// <returns></returns>
    private static DryData<(Expression Body, ParameterExpression Param)> GetExpressionInfo<TSource>(string keyName)
    {
        var type = typeof(TSource);
        var param = Expression.Parameter(type);
        var propertyNames = keyName.Split(".");
        Expression propertyAccess = param;
        foreach (var propertyName in propertyNames)
        {
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                return null;
            }
            type = property.PropertyType;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
        }
        return new DryData<(Expression, ParameterExpression)> { Data = (propertyAccess, param) };
    }

    /// <summary>
    /// 获取根据字段名获取Lambda表达式
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public static Expression<Func<TSource, TProperty>> GetKeySelector<TSource, TProperty>(string keyName)
    {
        var expressionInfo = GetExpressionInfo<TSource>(keyName);
        if (expressionInfo is null)
        {
            return null;
        }
        return Expression.Lambda<Func<TSource, TProperty>>(expressionInfo.Data.Body, expressionInfo.Data.Param);
    }
}