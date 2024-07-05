namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// 条件查询客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
public abstract class ApplicationQueryClientBase<TResult, TQuery> :
    ApiClientBase,
    IApplicationQueryService<TResult, TQuery>
    where TResult : IResultDto
    where TQuery : IQueryDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync(TQuery? queryDto)
        => await RequestAsync<bool>(HttpMethod.Get, "/Any", queryDto);

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<int> CountAsync(TQuery? queryDto)
        => await RequestAsync<int>(HttpMethod.Get, "/Count", queryDto);

    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult?> FirstAsync(TQuery? queryDto)
        => await RequestAsync<TResult>(HttpMethod.Get, "/First", queryDto);

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult[]> ArrayAsync(TQuery? queryDto)
        => (await RequestAsync<TResult[]>(HttpMethod.Get, null, queryDto))!;

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<PagedResult<TResult>> ArrayAsync(PagedQuery<TQuery> queryDto)
        => (await RequestAsync<PagedResult<TResult>>(HttpMethod.Get, "/Paged", queryDto))!;
}

/// <summary>
/// 条件查询客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryClientBase<TResult, TQuery, TKey> :
    ApplicationQueryClientBase<TResult, TQuery>,
    IApplicationQueryService<TResult, TQuery, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult?> FindAsync(TKey id)
        => await RequestAsync<TResult>(HttpMethod.Get, $"/{id}");
}

/// <summary>
/// 条件查、增、改、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryClientBase<TResult, TQuery, TCreate, TEdit, TKey> :
    ApplicationQueryCreateEditClientBase<TResult, TQuery, TCreate, TEdit, TKey>,
    IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
        => (await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}"))!;
}