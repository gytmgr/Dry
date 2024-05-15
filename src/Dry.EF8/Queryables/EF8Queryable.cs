namespace Dry.EF8.Queryables;

/// <summary>
/// EF8查询
/// </summary>
public class EF8Queryable : EFQueryable
{
#if NET8_0_OR_GREATER

    /// <summary>
    /// 指定索引
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public override async Task<TSource> ElementAtAsync<TSource>(IQueryable<TSource> queryable, int index)
        => await queryable.ElementAtAsync(index);

    /// <summary>
    /// 指定索引或默认
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public override async Task<TSource> ElementAtOrDefaultAsync<TSource>(IQueryable<TSource> queryable, int index)
        => await queryable.ElementAtOrDefaultAsync(index);

    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="queryable"></param>
    /// <returns></returns>
    public override async Task<int> ExecuteDeleteAsync<TSource>(IQueryable<TSource> queryable)
        => await queryable.ExecuteDeleteAsync();

#endif
}