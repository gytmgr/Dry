namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础查、改、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditDeleteController<TService, TResult, TEdit, TKey> :
    ApplicationEditController<TService, TResult, TEdit, TKey>,
    IApplicationEditDeleteService<TResult, TEdit, TKey>
    where TService : IApplicationEditDeleteService<TResult, TEdit, TKey>
    where TResult : IResultDto
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

/// <summary>
/// 件查、改、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditDeleteController<TService, TResult, TQuery, TEdit, TKey> :
    ApplicationQueryEditController<TService, TResult, TQuery, TEdit, TKey>,
    IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey>
    where TService : IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
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