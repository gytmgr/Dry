namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查询客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
public abstract class ApplicationClient<TResult> :
    ApiClient,
    IApplicationService<TResult>
    where TResult : IResultDto
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync()
        => await RequestAsync<bool>(HttpMethod.Get, "/Any");

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <returns></returns>
    public virtual async Task<int> CountAsync()
        => await RequestAsync<int>(HttpMethod.Get, "/Count");

    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <returns></returns>
    public virtual async Task<TResult> FirstAsync()
        => await RequestAsync<TResult>(HttpMethod.Get, "/First");

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public virtual async Task<TResult[]> ArrayAsync()
        => await RequestAsync<TResult[]>(HttpMethod.Get);

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    public virtual async Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery queryDto)
        => await RequestAsync<PagedResult<TResult>>(HttpMethod.Get, "/Paged", queryDto);
}

/// <summary>
/// 基础查询客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationClient<TResult, TKey> :
    ApplicationClient<TResult>,
    IApplicationService<TResult, TKey>
    where TResult : IResultDto
{
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> FindAsync([NotNull] TKey id)
        => await RequestAsync<TResult>(HttpMethod.Get, $"/{id}");
}

/// <summary>
/// 基础查、增、改、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationClient<TResult, TCreate, TEdit, TKey> :
    ApplicationCreateEditClient<TResult, TCreate, TEdit, TKey>,
    IApplicationService<TResult, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
        => await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
}