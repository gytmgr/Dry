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
}