namespace Dry.EF.Repositories;

/// <summary>
/// ef仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class, IEntity, IBoundedContext
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="provider"></param>
    public Repository(IServiceProvider provider) : base(provider) { }

    #region Queryable

    /// <summary>
    /// 获取查询
    /// </summary>
    /// <returns></returns>
    public override IQueryable<TEntity> GetQueryable()
        => _context.Set<TEntity>().AsQueryable();

    #endregion

    #region Tracking

    /// <summary>
    /// 属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public virtual bool PropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression)
        => _context.Entry(entitiy).Property(propertyExpression).IsModified;

    /// <summary>
    /// 单数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public virtual bool SingleNavigationPropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class
        => _context.Entry(entitiy).Reference(propertyExpression).IsModified;

    /// <summary>
    /// 复数导航属性是否更改
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public virtual bool ArrayNavigationPropertyModified<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class
        => _context.Entry(entitiy).Collection(propertyExpression).IsModified;

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public virtual async Task SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression) where TProperty : class
        => await _context.Entry(entitiy).Reference(propertyExpression).LoadAsync();

    /// <summary>
    /// 单数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    public virtual async Task<TProperty> SinglePropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, TProperty>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class
    {
        var queryable = _context.Entry(entitiy).Reference(propertyExpression).Query();
        if (paths is not null)
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
        }
        return await queryable.FirstOrDefaultAsync();
    }

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public virtual async Task ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression) where TProperty : class
        => await _context.Entry(entitiy).Collection(propertyExpression).LoadAsync();

    /// <summary>
    /// 复数属性延迟加载
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="entitiy"></param>
    /// <param name="propertyExpression"></param>
    /// <param name="paths"></param>
    /// <returns></returns>
    public virtual async Task<TProperty[]> ArrayPropertyLazyLoadAsync<TProperty>([NotNull] TEntity entitiy, [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, [NotNull] params Expression<Func<TProperty, dynamic>>[] paths) where TProperty : class
    {
        var queryable = _context.Entry(entitiy).Collection(propertyExpression).Query();
        if (paths is not null)
        {
            foreach (var path in paths)
            {
                queryable = queryable.Include(path);
            }
        }
        return await queryable.ToArrayAsync();
    }

    #endregion

    #region Add

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task AddAsync([NotNull] params TEntity[] entities)
        => await _context.AddRangeAsync(entities);

    #endregion

    #region Update

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task UpdateAsync([NotNull] params TEntity[] entities)
    {
        _context.UpdateRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 条件更新
    /// </summary>
    /// <param name="set"></param>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync([NotNull] Action<TEntity> set, params Expression<Func<TEntity, bool>>[] predicates)
    {
        var entities = await _context.Set<TEntity>().Where(predicates).ToArrayAsync();
        foreach (var entity in entities)
        {
            set(entity);
        }
    }

    #endregion

    #region Remove

    /// <summary>
    /// 主键删除
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    public virtual async Task RemoveAsync([NotNull] params object[] keyValues)
    {
        var entity = await _context.FindAsync<TEntity>(keyValues);
        if (entity is not null)
        {
            _context.Remove(entity);
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual Task RemoveAsync([NotNull] params TEntity[] entities)
    {
        _context.RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 条件删除
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public virtual async Task RemoveAsync(params Expression<Func<TEntity, bool>>[] predicates)
    {
        var entities = await _context.Set<TEntity>().Where(predicates).ToArrayAsync();
        _context.RemoveRange(entities);
    }

    #endregion

    #region Find

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindAsync([NotNull] params object[] keyValues)
        => await _context.FindAsync<TEntity>(keyValues);

    #endregion
}