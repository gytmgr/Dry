namespace Dry.Application.Contracts.Services;

/// <summary>
/// 条件查询应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
public interface IApplicationQueryService<TResult, TQuery>
    where TResult : IResultDto
    where TQuery : IQueryDto
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(TQuery? queryDto = default);

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    Task<int> CountAsync(TQuery? queryDto = default);

    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    Task<TResult?> FirstAsync(TQuery? queryDto = default);

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    Task<TResult[]> ArrayAsync(TQuery? queryDto = default);

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    Task<PagedResult<TResult>> ArrayAsync(PagedQuery<TQuery> queryDto);
}

/// <summary>
/// 条件查询应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationQueryService<TResult, TQuery, TKey> :
    IApplicationQueryService<TResult, TQuery>,
    IFindService<TResult, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
{ }

/// <summary>
/// 条件查、增、改、删应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> :
    IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>,
    IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>,
    IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{ }