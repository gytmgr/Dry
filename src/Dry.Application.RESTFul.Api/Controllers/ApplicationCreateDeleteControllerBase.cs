namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础查、增、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateDeleteControllerBase<TService, TResult, TCreate, TKey> :
    ApplicationCreateControllerBase<TService, TResult, TCreate, TKey>,
    IApplicationCreateDeleteService<TResult, TCreate, TKey>
    where TService : IApplicationCreateDeleteService<TResult, TCreate, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
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

/// <summary>
/// 条件查、增、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateDeleteControllerBase<TService, TResult, TQuery, TCreate, TKey> :
    ApplicationQueryCreateControllerBase<TService, TResult, TQuery, TCreate, TKey>,
    IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>
    where TService : IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
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