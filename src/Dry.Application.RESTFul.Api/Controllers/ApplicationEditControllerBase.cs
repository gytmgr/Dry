﻿namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础查、改应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditControllerBase<TService, TResult, TEdit, TKey> :
    ApplicationControllerBase<TService, TResult, TKey>,
    IApplicationEditService<TResult, TEdit, TKey>
    where TService : IApplicationEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public virtual async Task<TResult> EditAsync(TKey id, [FromBody][BindRequired] TEdit editDto)
        => await AppService.EditAsync(id, editDto);
}

/// <summary>
/// 条件查、改应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditControllerBase<TService, TResult, TQuery, TEdit, TKey> :
    ApplicationQueryControllerBase<TService, TResult, TQuery, TKey>,
    IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
    where TService : IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public virtual async Task<TResult> EditAsync(TKey id, [FromBody][BindRequired] TEdit editDto)
        => await AppService.EditAsync(id, editDto);
}