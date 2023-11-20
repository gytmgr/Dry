namespace Dry.Application.Services;

/// <summary>
/// 基础查、删应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationDeleteService<TBoundedContext, TEntity, TResult, TKey> :
    ApplicationService<TEntity, TResult, TKey>,
    IApplicationDeleteService<TResult, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
{
    /// <summary>
    /// 工作单元
    /// </summary>
    protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
        => _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork<TBoundedContext>>();

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
/// 条件查、删应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryDeleteService<TBoundedContext, TEntity, TResult, TQuery, TKey> :
    ApplicationQueryService<TEntity, TResult, TQuery, TKey>,
    IApplicationQueryDeleteService<TResult, TQuery, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
{
    /// <summary>
    /// 工作单元
    /// </summary>
    protected readonly IUnitOfWork<TBoundedContext> _unitOfWork;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryDeleteService(IServiceProvider serviceProvider) : base(serviceProvider)
        => _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork<TBoundedContext>>();

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