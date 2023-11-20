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
    public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector)[]? orderBys)
    {
        query.CheckParamNull(nameof(query));

        if (orderBys?.Length > 0)
        {
            var orderQuery = default(IOrderedEnumerable<TSource>);
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
    public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> query, params (bool isAsc, Func<TSource, dynamic> keySelector, IComparer<dynamic> comparer)[]? orderBys)
    {
        query.CheckParamNull(nameof(query));

        if (orderBys?.Length > 0)
        {
            var orderQuery = default(IOrderedEnumerable<TSource>);
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
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource?, TSource?, bool> equalFunc)
    {
        first.CheckParamNull(nameof(first));
        second.CheckParamNull(nameof(second));

        return first.Except(second, new CustomEqualityComparer<TSource>(equalFunc));
    }

    /// <summary>
    /// 构建树结构
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="rootList">根列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="idField">id字段的名称</param>
    /// <param name="parentField">父id字段的名称</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildTree<TSource, TResult>(this IEnumerable<TSource> rootList, IEnumerable<TSource> allList, string idField, string parentField, Func<TSource, IEnumerable<TResult>?, TResult> selector, Predicate<TSource>? exclude = null)
    {
        rootList.CheckParamNull(nameof(rootList));
        allList.CheckParamNull(nameof(allList));
        idField.CheckParamNull(nameof(idField));
        parentField.CheckParamNull(nameof(parentField));
        selector.CheckParamNull(nameof(selector));

        var type = typeof(TSource);
        var idProperty = type.GetProperty(idField);
        var parentProperty = type.GetProperty(parentField);
        var result = new List<TResult>();
        foreach (var current in rootList)
        {
            if (exclude is not null && exclude(current))
            {
                continue;
            }
            var children = allList.Where(x =>
            {
                var id = idProperty?.GetValue(current, null);
                var parentID = parentProperty?.GetValue(x, null);
                return id?.Equals(parentID) ?? false;
            });
            var childrenTree = BuildTree(children, allList, idField, parentField, selector, exclude);
            result.Add(selector(current, childrenTree));
        }
        return result;
    }

    /// <summary>
    /// 构建值类型主键的树结构
    /// </summary>
    /// <typeparam name="TSource">ITree接口实现类型</typeparam>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <typeparam name="TType">TSource的id类型</typeparam>
    /// <param name="rootList">根列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildTree<TSource, TResult, TType>(this IEnumerable<TSource> rootList, IEnumerable<TSource> allList, Func<TSource, IEnumerable<TResult>?, TResult> selector, Predicate<TSource>? exclude = null) where TSource : ITree<TType> where TType : struct
        => rootList.BuildTree(allList, nameof(ITree<TType>.Id), nameof(ITree<TType>.ParentId), selector, exclude);

    /// <summary>
    /// 构建string类型主键的树结构
    /// </summary>
    /// <typeparam name="TSource">IStringTree接口实现类型</typeparam>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="rootList">根列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildStringTree<TSource, TResult>(this IEnumerable<TSource> rootList, IEnumerable<TSource> allList, Func<TSource, IEnumerable<TResult>?, TResult> selector, Predicate<TSource>? exclude = null) where TSource : IStringTree
        => rootList.BuildTree(allList, nameof(IStringTree.Id), nameof(IStringTree.ParentId), selector, exclude);
}