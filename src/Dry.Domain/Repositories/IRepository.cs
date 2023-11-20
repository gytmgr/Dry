namespace Dry.Domain.Repositories;

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity, IBoundedContext
{
    #region Tracking

    /// <summary>
    /// 是否更改
    /// </summary>
    /// <param name="entitiy"></param>
    /// <returns></returns>
    bool Modified(TEntity entitiy);

    /// <summary>
    /// 属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool PropertyModified<TProperty>(TEntity entitiy, Expression<Func<TEntity, TProperty>> propertyExpression);

    /// <summary>
    /// 单数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool SingleNavigationPropertyModified<TProperty>(TEntity entitiy, Expression<Func<TEntity, TProperty?>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 复数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    bool ArrayNavigationPropertyModified<TProperty>(TEntity entitiy, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    Task SinglePropertyLazyLoadAsync<TProperty>(TEntity entitiy, Expression<Func<TEntity, TProperty?>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    Task<TProperty?> SinglePropertyLazyLoadAsync<TProperty>(TEntity entitiy, Expression<Func<TEntity, TProperty?>> propertyExpression, params Expression<Func<TProperty, dynamic>>[]? paths) where TProperty : class;

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    Task ArrayPropertyLazyLoadAsync<TProperty>(TEntity entitiy, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class;

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    Task<TProperty[]> ArrayPropertyLazyLoadAsync<TProperty>(TEntity entitiy, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, params Expression<Func<TProperty, dynamic>>[]? paths) where TProperty : class;

    #endregion

    #region Add

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task AddAsync(params TEntity[] entities);

    #endregion

    #region Update

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task UpdateAsync(params TEntity[] entities);

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="set"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    Task UpdateAsync(Action<TEntity> set, params Expression<Func<TEntity, bool>>[]? predicates);

    #endregion

    #region Remove

    /// <summary>
    /// 主键删除
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task RemoveAsync(params object[] keyValues);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task RemoveAsync(params TEntity[] entities);

    /// <summary>
    /// 条件删除
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    Task RemoveAsync(params Expression<Func<TEntity, bool>>[]? predicates);

    #endregion

    #region Find

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task<TEntity?> FindAsync(params object[] keyValues);

    #endregion
}