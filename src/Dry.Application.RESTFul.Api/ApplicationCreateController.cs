namespace Dry.Application.RESTFul.Api;

/// <summary>
/// 基础查、增应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationCreateController<TService, TResult, TCreate> :
    ApplicationController<TService, TResult>,
    IApplicationCreateService<TResult, TCreate>
    where TService : IApplicationCreateService<TResult, TCreate>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        => await AppService.CreateAsync(createDto);
}

/// <summary>
/// 基础查、增应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateController<TService, TResult, TCreate, TKey> :
    ApplicationController<TService, TResult, TKey>,
    IApplicationCreateService<TResult, TCreate, TKey>
    where TService : IApplicationCreateService<TResult, TCreate, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        => await AppService.CreateAsync(createDto);
}

/// <summary>
/// 条件查、增应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationQueryCreateController<TService, TResult, TQuery, TCreate> :
    ApplicationQueryController<TService, TResult, TQuery>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate>
    where TService : IApplicationQueryCreateService<TResult, TQuery, TCreate>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        => await AppService.CreateAsync(createDto);
}

/// <summary>
/// 条件查、增应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateController<TService, TResult, TQuery, TCreate, TKey> :
    ApplicationQueryController<TService, TResult, TQuery, TKey>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>
    where TService : IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
{
    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<TResult> CreateAsync([FromBody] TCreate createDto)
        => await AppService.CreateAsync(createDto);
}