#nullable enable

namespace Dry.Domain.Extensions;

/// <summary>
/// 仓储扩展
/// </summary>
public static class RepositoryExtension
{
    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static async Task<TEntity?> FirstAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>>? predicate = null) where TEntity : IEntity, IBoundedContext
        => await repository.FirstAsync(predicate, paths: null, null);

    /// <summary>
    /// 条件查询第一条并提前加载导航属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    public static async Task<TEntity?> FirstAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths) where TEntity : IEntity, IBoundedContext
        => await repository.FirstAsync(predicate, paths, null);

    /// <summary>
    /// 条件查询第一条并排序
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static async Task<TEntity?> FirstAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys) where TEntity : IEntity, IBoundedContext
        => await repository.FirstAsync(predicate, paths: null, orderBys);

    /// <summary>
    /// 查询第一条指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static async Task<TResult?> FirstAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, TResult>> selector) where TEntity : IEntity, IBoundedContext
        => await repository.FirstAsync(selector, null);

    /// <summary>
    /// 排序查询第一条指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static async Task<TResult?> FirstAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys) where TEntity : IEntity, IBoundedContext
        => await repository.FirstAsync(selector, null, orderBys);

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static async Task<TEntity[]> ToArrayAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>>? predicate = null) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(predicate, paths: null, null);

    /// <summary>
    /// 条件查询并提前加载导航属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    public static async Task<TEntity[]> ToArrayAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(predicate, paths, null);

    /// <summary>
    /// 条件查询并排序
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static async Task<TEntity[]> ToArrayAsync<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(predicate, paths: null, orderBys);

    /// <summary>
    /// 查询指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static async Task<TResult[]> ToArrayAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, TResult>> selector) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(selector, null);

    /// <summary>
    /// 排序查询指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static async Task<TResult[]> ToArrayAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, TResult>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(selector, null, orderBys);

    /// <summary>
    /// 查询指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static async Task<TResult[]> ToArrayAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, IEnumerable<TResult>>> selector) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(selector, null);

    /// <summary>
    /// 排序查询指定字段
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="repository"></param>
    /// <param name="selector"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    public static async Task<TResult[]> ToArrayAsync<TEntity, TResult>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, IEnumerable<TResult>>> selector, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys) where TEntity : IEntity, IBoundedContext
        => await repository.ToArrayAsync(selector, null, orderBys);
}