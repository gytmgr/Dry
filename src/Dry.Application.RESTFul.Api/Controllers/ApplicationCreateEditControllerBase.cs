namespace Dry.Application.RESTFul.Api.Controllers;

/// <summary>
/// 基础查、增、改应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateEditControllerBase<TService, TResult, TCreate, TEdit, TKey> :
    ApplicationCreateControllerBase<TService, TResult, TCreate, TKey>,
    IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
    where TService : IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
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
/// 条件查、增、改应用控制器
/// </summary>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateEditControllerBase<TService, TResult, TQuery, TCreate, TEdit, TKey> :
    ApplicationQueryCreateControllerBase<TService, TResult, TQuery, TCreate, TKey>,
    IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
    where TService : IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
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