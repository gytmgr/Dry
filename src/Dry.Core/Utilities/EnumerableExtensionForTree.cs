namespace Dry.Core.Utilities;

/// <summary>
/// 树状结构扩展
/// </summary>
public static class EnumerableExtensionForTree
{
    /// <summary>
    /// 构建树结构
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="currentList">当前列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="idField">id字段的名称</param>
    /// <param name="parentField">父id字段的名称</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildTree<TSource, TResult>(this IEnumerable<TSource> currentList, IEnumerable<TSource> allList, string idField, string parentField, Func<TSource, IEnumerable<TResult>, TResult> selector, Predicate<TSource> exclude = null)
    {
        if (currentList == null || allList == null)
        {
            return null;
        }
        var type = typeof(TSource);
        var idProperty = type.GetProperty(idField);
        if (idProperty == null)
        {
            return null;
        }
        var parentProperty = type.GetProperty(parentField);
        if (parentProperty == null)
        {
            return null;
        }
        var result = new List<TResult>();
        foreach (var current in currentList)
        {
            if (exclude != null && exclude(current))
            {
                continue;
            }
            var children = allList.Where(x =>
            {
                var id = idProperty.GetValue(current, null);
                var parentID = parentProperty.GetValue(x, null);
                return id.Equals(parentID);
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
    /// <param name="currentList">当前列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildTree<TSource, TResult, TType>(this IEnumerable<TSource> currentList, IEnumerable<TSource> allList, Func<TSource, IEnumerable<TResult>, TResult> selector, Predicate<TSource> exclude = null) where TSource : ITree<TType> where TType : struct
    {
        return currentList.BuildTree(allList, nameof(ITree<TType>.Id), nameof(ITree<TType>.ParentId), selector, exclude);
    }

    /// <summary>
    /// 构建string类型主键的树结构
    /// </summary>
    /// <typeparam name="TSource">IStringTree接口实现类型</typeparam>
    /// <typeparam name="TResult">结果类型</typeparam>
    /// <param name="currentList">当前列表</param>
    /// <param name="allList">所有列表</param>
    /// <param name="selector">结果构造器</param>
    /// <param name="exclude">排除条件</param>
    /// <returns></returns>
    public static IEnumerable<TResult> BuildStringTree<TSource, TResult>(this IEnumerable<TSource> currentList, IEnumerable<TSource> allList, Func<TSource, IEnumerable<TResult>, TResult> selector, Predicate<TSource> exclude = null) where TSource : IStringTree
    {
        return currentList.BuildTree(allList, nameof(IStringTree.Id), nameof(IStringTree.ParentId), selector, exclude);
    }
}