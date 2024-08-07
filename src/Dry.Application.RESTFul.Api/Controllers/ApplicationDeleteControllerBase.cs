﻿namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础查、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationDeleteControllerBase<TService, TResult, TKey> :
    ApplicationControllerBase<TService, TResult, TKey>,
    IApplicationDeleteService<TResult, TKey>
    where TService : IApplicationDeleteService<TResult, TKey>
    where TResult : IResultDto
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
/// 条件查、删应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryDeleteControllerBase<TService, TResult, TQuery, TKey> :
    ApplicationQueryControllerBase<TService, TResult, TQuery, TKey>,
    IApplicationQueryDeleteService<TResult, TQuery, TKey>
    where TService : IApplicationQueryDeleteService<TResult, TQuery, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
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