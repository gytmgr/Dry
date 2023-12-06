namespace Dry.Application.Services;

/// <summary>
/// 基础查、增应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationCreateServiceBase<TBoundedContext, TEntity, TResult, TCreate> :
    ApplicationServiceBase<TEntity, TResult>,
    IApplicationCreateService<TResult, TCreate>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot, TBoundedContext
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }

    /// <summary>
    /// 映射实体新建数据
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual Task<TEntity> MapCreateEntityAsync(TCreate createDto)
        => Task.FromResult(_mapper.Map<TEntity>(createDto));

    /// <summary>
    /// 配置实体新建数据
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity)
        {
            await createEntity.CreateAsync(_serviceProvider);
        }
        if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
        {
            hasAddTimeEntity.AddTime = DateTime.Now;
        }
    }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
    {
        var entity = await MapCreateEntityAsync(createDto);
        await SetCreateEntityAsync(entity, createDto);
        await _repository.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
        await CreatedAsync(entity, createDto);
        return _mapper.Map<TResult>(entity);
    }

    /// <summary>
    /// 新建完成处理
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
        {
            await _unitOfWork.CompleteAsync();
        }
    }
}

/// <summary>
/// 基础查、增应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateServiceBase<TBoundedContext, TEntity, TResult, TCreate, TKey> :
    ApplicationCreateServiceBase<TBoundedContext, TEntity, TResult, TCreate>,
    IApplicationCreateService<TResult, TCreate, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult?> FindAsync(TKey id)
    {
        var entity = await _repository.FindAsync(id!);
        if (entity is not null)
        {
            return await SingleResultMapAsync(entity);
        }
        return default;
    }
}

/// <summary>
/// 条件查、增应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationQueryCreateServiceBase<TBoundedContext, TEntity, TResult, TQuery, TCreate> :
    ApplicationQueryServiceBase<TEntity, TResult, TQuery>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot, TBoundedContext
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }

    /// <summary>
    /// 映射实体新建数据
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual Task<TEntity> MapCreateEntityAsync(TCreate createDto)
        => Task.FromResult(_mapper.Map<TEntity>(createDto));

    /// <summary>
    /// 配置实体新建数据
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity)
        {
            await createEntity.CreateAsync(_serviceProvider);
        }
        if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
        {
            hasAddTimeEntity.AddTime = DateTime.Now;
        }
    }

    /// <summary>
    /// 新建后处理
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
    {
        var entity = await MapCreateEntityAsync(createDto);
        await SetCreateEntityAsync(entity, createDto);
        await _repository.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
        await CreatedAsync(entity, createDto);
        return _mapper.Map<TResult>(entity);
    }
}

/// <summary>
/// 条件查、增应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateServiceBase<TBoundedContext, TEntity, TResult, TQuery, TCreate, TKey> :
    ApplicationQueryServiceBase<TEntity, TResult, TQuery, TKey>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }

    /// <summary>
    /// 映射实体新建数据
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual Task<TEntity> MapCreateEntityAsync(TCreate createDto)
        => Task.FromResult(_mapper.Map<TEntity>(createDto));

    /// <summary>
    /// 配置实体新建数据
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task SetCreateEntityAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity)
        {
            await createEntity.CreateAsync(_serviceProvider);
        }
        if (entity is IHasAddTime hasAddTimeEntity && hasAddTimeEntity.AddTime == default)
        {
            hasAddTimeEntity.AddTime = DateTime.Now;
        }
    }

    /// <summary>
    /// 新建后处理
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="createDto"></param>
    /// <returns></returns>
    protected virtual async Task CreatedAsync(TEntity entity, TCreate createDto)
    {
        if (entity is ICreate createEntity && await createEntity.CreatedAsync(_serviceProvider))
        {
            await _unitOfWork.CompleteAsync();
        }
    }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
    {
        var entity = await MapCreateEntityAsync(createDto);
        await SetCreateEntityAsync(entity, createDto);
        await _repository.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
        await CreatedAsync(entity, createDto);
        return _mapper.Map<TResult>(entity);
    }
}