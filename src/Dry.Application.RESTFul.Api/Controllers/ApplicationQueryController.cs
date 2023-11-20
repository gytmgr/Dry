namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 条件查询应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
public abstract class ApplicationQueryController<TService, TResult, TQuery> :
    ApplicationController<TService>,
    IApplicationQueryService<TResult, TQuery>
    where TService : IApplicationQueryService<TResult, TQuery>
    where TResult : IResultDto
    where TQuery : IQueryDto
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet("Any")]
    public virtual async Task<bool> AnyAsync([FromQuery] TQuery? queryDto)
        => await AppService.AnyAsync(queryDto);

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet("Count")]
    public virtual async Task<int> CountAsync([FromQuery] TQuery? queryDto)
        => await AppService.CountAsync(queryDto);

    /// <summary>
    /// 条件查询第一条
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet("First")]
    public virtual async Task<TResult?> FirstAsync([FromQuery] TQuery? queryDto)
        => await AppService.FirstAsync(queryDto);

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<TResult[]> ArrayAsync([FromQuery] TQuery? queryDto)
        => await AppService.ArrayAsync(queryDto);

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet("Paged")]
    public virtual async Task<PagedResult<TResult>> ArrayAsync([FromQuery][BindRequired] PagedQuery<TQuery> queryDto)
        => await AppService.ArrayAsync(queryDto);
}

/// <summary>
/// 条件查询应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryController<TService, TResult, TQuery, TKey> :
    ApplicationQueryController<TService, TResult, TQuery>,
    IApplicationQueryService<TResult, TQuery, TKey>
    where TService : IApplicationQueryService<TResult, TQuery, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
{
    /// <summary>
    /// 主键查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public virtual async Task<TResult?> FindAsync(TKey id)
        => await AppService.FindAsync(id);
}

/// <summary>
/// 条件查、增、改、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryController<TService, TResult, TQuery, TCreate, TEdit, TKey> :
    ApplicationQueryCreateEditController<TService, TResult, TQuery, TCreate, TEdit, TKey>,
    IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
    where TService : IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public virtual async Task<TResult> DeleteAsync(TKey id)
        => await AppService.DeleteAsync(id);
}