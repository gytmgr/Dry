namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
public abstract class ApplicationController<TService> : DryController
{
    /// <summary>
    /// 应用服务
    /// </summary>
    protected virtual TService AppService
        => Service<TService>() ?? throw new NullDataBizException();
}

/// <summary>
/// 基础查询应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class ApplicationController<TService, TResult> :
    ApplicationController<TService>,
    IApplicationService<TResult>
    where TService : IApplicationService<TResult>
    where TResult : IResultDto
{
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <returns></returns>
    [HttpGet("Any")]
    public virtual async Task<bool> AnyAsync()
        => await AppService.AnyAsync();

    /// <summary>
    /// 数量查询
    /// </summary>
    /// <returns></returns>
    [HttpGet("Count")]
    public virtual async Task<int> CountAsync()
        => await AppService.CountAsync();

    /// <summary>
    /// 查询第一条
    /// </summary>
    /// <returns></returns>
    [HttpGet("First")]
    public virtual async Task<TResult?> FirstAsync()
        => await AppService.FirstAsync();

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<TResult[]> ArrayAsync()
        => await AppService.ArrayAsync();

    /// <summary>
    /// 分页条件查询
    /// </summary>
    /// <param name="queryDto"></param>
    /// <returns></returns>
    [HttpGet("Paged")]
    public virtual async Task<PagedResult<TResult>> ArrayAsync([FromQuery][BindRequired] PagedQuery queryDto)
        => await AppService.ArrayAsync(queryDto);
}

/// <summary>
/// 基础查询应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationController<TService, TResult, TKey> :
    ApplicationController<TService, TResult>,
    IApplicationService<TResult, TKey>
    where TService : IApplicationService<TResult, TKey>
    where TResult : IResultDto
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
/// 基础查、增、改、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationController<TService, TResult, TCreate, TEdit, TKey> :
    ApplicationCreateEditController<TService, TResult, TCreate, TEdit, TKey>,
    IApplicationService<TResult, TCreate, TEdit, TKey>
    where TService : IApplicationService<TResult, TCreate, TEdit, TKey>
    where TResult : IResultDto
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