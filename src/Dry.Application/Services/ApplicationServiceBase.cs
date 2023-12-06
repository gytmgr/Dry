namespace Dry.Application.Services;

/// <summary>
/// 应用服务
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class ApplicationServiceBase<TEntity> where TEntity : class, IAggregateRoot, IBoundedContext
{
    /// <summary>
    /// 服务生成器
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 对象映射
    /// </summary>
    protected readonly IMapper _mapper;

    /// <summary>
    /// 只读仓储
    /// </summary>
    protected readonly IReadOnlyRepository<TEntity> _readOnlyRepository;

    /// <summary>
    /// 仓储
    /// </summary>
    protected readonly IRepository<TEntity> _repository;

    /// <summary>
    /// 工作单元
    /// </summary>
    protected readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationServiceBase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _readOnlyRepository = serviceProvider.GetReadOnlyRepository<TEntity>();
        _repository = serviceProvider.GetRepository<TEntity>();
        _unitOfWork = serviceProvider.GetUnitOfWork<TEntity>();
    }

    /// <summary>
    /// 获取只读仓储
    /// </summary>
    /// <typeparam name="TOtherEntity"></typeparam>
    /// <returns></returns>
    protected virtual IReadOnlyRepository<TOtherEntity> ReadOnlyRepository<TOtherEntity>() where TOtherEntity : class, IEntity, IBoundedContext
        => _serviceProvider.GetReadOnlyRepository<TOtherEntity>();

    /// <summary>
    /// 获取仓储
    /// </summary>
    /// <typeparam name="TOtherEntity"></typeparam>
    /// <returns></returns>
    protected virtual IRepository<TOtherEntity> Repository<TOtherEntity>() where TOtherEntity : class, IEntity, IBoundedContext
        => _serviceProvider.GetRepository<TOtherEntity>();

    /// <summary>
    /// 获取属性加载表达式
    /// </summary>
    /// <returns></returns>
    protected virtual Expression<Func<TEntity, dynamic>>[] GetPropertyLoads()
        => Array.Empty<Expression<Func<TEntity, dynamic>>>();

    /// <summary>
    /// 获取属性加载表达式
    /// </summary>
    /// <returns></returns>
    protected virtual Task<Expression<Func<TEntity, dynamic>>[]> GetPropertyLoadsAsync()
        => Task.FromResult(GetPropertyLoads());

    /// <summary>
    /// 获取查询条件表达式
    /// </summary>
    /// <returns></returns>
    protected virtual Expression<Func<TEntity, bool>>[] GetPredicates()
        => Array.Empty<Expression<Func<TEntity, bool>>>();

    /// <summary>
    /// 获取查询条件表达式
    /// </summary>
    /// <returns></returns>
    protected virtual Task<Expression<Func<TEntity, bool>>[]> GetPredicatesAsync()
        => Task.FromResult(GetPredicates());

    /// <summary>
    /// 获取排序表达式
    /// </summary>
    /// <returns></returns>
    protected virtual (bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[] GetOrderBys()
        => Array.Empty<(bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)>();

    /// <summary>
    /// 获取排序表达式
    /// </summary>
    /// <returns></returns>
    protected virtual Task<(bool isAsc, Expression<Func<TEntity, dynamic>> keySelector)[]> GetOrderBysAsync()
        => Task.FromResult(GetOrderBys());
}

/// <summary>
/// 应用服务
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class ApplicationServiceBase<TEntity, TResult> :
    ApplicationServiceBase<TEntity>,
    IApplicationService<TResult>
    where TEntity : class, IAggregateRoot, IBoundedContext
    where TResult : IResultDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 单一结果映射
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected virtual Task<TResult> SingleResultMapAsync(TEntity entity)
    {
        var result = _mapper.Map<TResult>(entity);
        return Task.FromResult(result);
    }

    /// <summary>
    /// 多结果映射
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected virtual Task<TResult[]> ArrayResultMapAsync(TEntity[] entities)
    {
        var result = _mapper.Map<TResult[]>(entities);
        return Task.FromResult(result);
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync()
    {
        var predicates = await GetPredicatesAsync();
        return await _readOnlyRepository.AnyAsync(predicates);
    }

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <returns></returns>
    public virtual async Task<int> CountAsync()
    {
        var predicates = await GetPredicatesAsync();
        return await _readOnlyRepository.CountAsync(predicates);
    }

    /// <summary>
    /// 查询第一条
    /// </summary>
    /// <returns></returns>
    public virtual async Task<TResult?> FirstAsync()
    {
        var propertyLoads = await GetPropertyLoadsAsync();
        var predicates = await GetPredicatesAsync();
        var orderBys = await GetOrderBysAsync();
        var entity = await _readOnlyRepository.GetQueryable().Include(propertyLoads).Where(predicates).OrderBy(orderBys).FirstOrDefaultAsync();
        if (entity is not null)
        {
            return await SingleResultMapAsync(entity);
        }
        return default;
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public virtual async Task<TResult[]> ArrayAsync()
    {
        var propertyLoads = await GetPropertyLoadsAsync();
        var predicates = await GetPredicatesAsync();
        var orderBys = await GetOrderBysAsync();
        var entities = await _readOnlyRepository.GetQueryable().Include(propertyLoads).Where(predicates).OrderBy(orderBys).ToArrayAsync();
        return await ArrayResultMapAsync(entities);
    }

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<PagedResult<TResult>> ArrayAsync(PagedQuery queryDto)
    {
        var propertyLoads = await GetPropertyLoadsAsync();
        var predicates = await GetPredicatesAsync();
        var orderBys = await GetOrderBysAsync();
        var total = await _readOnlyRepository.CountAsync(predicates);
        var entities = await _readOnlyRepository.GetQueryable().Include(propertyLoads).Where(predicates).OrderBy(orderBys).Skip((queryDto.PageIndex - 1) * queryDto.PageSize).Take(queryDto.PageSize).ToArrayAsync();
        return new PagedResult<TResult>
        {
            Total = total,
            Items = await ArrayResultMapAsync(entities)
        };
    }
}

/// <summary>
/// 应用服务
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationServiceBase<TEntity, TResult, TKey> :
    ApplicationServiceBase<TEntity, TResult>,
    IApplicationService<TResult, TKey>
    where TEntity : class, IAggregateRoot<TKey>, IBoundedContext
    where TResult : IResultDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
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
/// 基础查、增、改、删应用服务接口
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationServiceBase<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey> :
    ApplicationCreateEditServiceBase<TBoundedContext, TEntity, TResult, TCreate, TEdit, TKey>,
    IApplicationService<TResult, TCreate, TEdit, TKey>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IAggregateRoot<TKey>, TBoundedContext
    where TResult : IResultDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationServiceBase(IServiceProvider serviceProvider) : base(serviceProvider)
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