namespace Dry.Domain.Repositories;

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : IEntity, IBoundedContext
{
    #region Tracking

    /// <summary>
    /// 属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool PropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression);

    /// <summary>
    /// 单数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool SingleNavigationPropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 复数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool ArrayNavigationPropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    Task SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    Task<TProperty> SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class;

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    Task ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    Task<TProperty[]> ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class;

    #endregion

    #region Add

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task AddAsync([NotNull] params TEntity[] entities);

    #endregion

    #region Update

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task UpdateAsync([NotNull] params TEntity[] entities);

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="set"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    Task UpdateAsync([NotNull] Action<TEntity> set, params Expression<Func<TEntity, bool>>[] predicates);

    #endregion

    #region Remove

    /// <summary>
    /// 主键删除
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task RemoveAsync([NotNull] params object[] keyValues);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task RemoveAsync([NotNull] params TEntity[] entities);

    /// <summary>
    /// 条件删除
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    Task RemoveAsync(params Expression<Func<TEntity, bool>>[] predicates);

    #endregion

    #region Find

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync([NotNull] params object[] keyValues);

    #endregion

    #region First

    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TEntity> FirstAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// 条件查询第一条并提前加载导航属性
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    [Obsolete("Use IncludeFirstAsync method.")]
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths);

    /// <summary>
    /// 条件查询第一条并排序提前加载导航属性
    /// </summary>
    /// <param name="predicates"></param>
    /// <param name="paths"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>>[] predicates, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 排序条件查询第一条指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TResult> FirstAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 自定义查询第一条并提前加载导航属性
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="func"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TResult> FirstAsync<TResult>([NotNull] Func<IQueryable<TEntity>, IQueryable<TResult>> func, params Expression<Func<TEntity, dynamic>>[] paths);

    #endregion

    #region ToArray

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="predicates"></param>
    [Obsolete("Use GetQueryable method to do.")]
    /// <returns></returns>
    Task<TEntity[]> ToArrayAsync(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// 条件查询并提前加载导航属性
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    [Obsolete("Use IncludeToArrayAsync method.")]
    Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] paths);

    /// <summary>
    /// 条件查询并排序提前加载导航属性
    /// </summary>
    /// <param name="predicates"></param>
    /// <param name="paths"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TEntity[]> ToArrayAsync(Expression<Func<TEntity, bool>>[] predicates, Expression<Func<TEntity, dynamic>>[] paths, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 排序条件查询指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 排序条件查询指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 排序条件查询指定字段
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="selector"></param>
    /// <param name="predicates"></param>
    /// <param name="orderBys"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TResult[]> ToArrayAsync<TResult>([NotNull] Expression<Func<TEntity, IEnumerable<TResult>>> selector, Expression<Func<TEntity, bool>>[] predicates, params (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] orderBys);

    /// <summary>
    /// 自定义查询并提前加载导航属性
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="func"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    [Obsolete("Use GetQueryable method to do.")]
    Task<TResult[]> ToArrayAsync<TResult>([NotNull] Func<IQueryable<TEntity>, IQueryable<TResult>> func, params Expression<Func<TEntity, dynamic>>[] paths);

    #endregion
}