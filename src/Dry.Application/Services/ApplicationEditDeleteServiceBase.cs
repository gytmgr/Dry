namespace Dry.Application.Services;

/// <summary>
/// 基础查、改、删应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditDeleteServiceBase<TBoundedContext, TEntity, TResult, TEdit, TKey> :
    ApplicationEditServiceBase<TBoundedContext, TEntity, TResult, TEdit, TKey>,
    IApplicationEditDeleteService<TResult, TEdit, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationEditDeleteServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 获取删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullDataBizException"></exception>
    protected virtual async Task<TEntity> GetDeleteEntityAsync(TKey id)
        => await _repository.FindAsync(id!) ?? throw new NullDataBizException();

    /// <summary>
    /// 配置实体删除数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual async Task SetDeleteEntityAsync(TEntity entity)
    {
        if (entity is IDelete deleteEntity)
        {
            await deleteEntity.DeleteAsync(_serviceProvider);
        }
    }

    /// <summary>
    /// 删除后处理
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual async Task DeletedAsync(TEntity entity)
    {
        if (entity is IDelete deleteEntity && await deleteEntity.DeletedAsync(_serviceProvider))
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
    {
        var entity = await GetDeleteEntityAsync(id);
        await SetDeleteEntityAsync(entity);
        await _repository.RemoveAsync(entity);
        await _unitOfWork.CompleteAsync();
        await DeletedAsync(entity);
        return _mapper.Map<TResult>(entity);
    }
}

/// <summary>
/// 条件查、改、删应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditDeleteServiceBase<TBoundedContext, TEntity, TResult, TQuery, TEdit, TKey> :
   ApplicationQueryEditServiceBase<TBoundedContext, TEntity, TResult, TQuery, TEdit, TKey>,
    IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryEditDeleteServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 获取删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullDataBizException"></exception>
    protected virtual async Task<TEntity> GetDeleteEntityAsync(TKey id)
        => await _repository.FindAsync(id!) ?? throw new NullDataBizException();

    /// <summary>
    /// 配置实体删除数据
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual async Task SetDeleteEntityAsync(TEntity entity)
    {
        if (entity is IDelete deleteEntity)
        {
            await deleteEntity.DeleteAsync(_serviceProvider);
        }
    }

    /// <summary>
    /// 删除后处理
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual async Task DeletedAsync(TEntity entity)
    {
        if (entity is IDelete deleteEntity && await deleteEntity.DeletedAsync(_serviceProvider))
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
    {
        var entity = await GetDeleteEntityAsync(id);
        await SetDeleteEntityAsync(entity);
        await _repository.RemoveAsync(entity);
        await _unitOfWork.CompleteAsync();
        await DeletedAsync(entity);
        return _mapper.Map<TResult>(entity);
    }
}