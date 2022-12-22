namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查、增、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateEditClient<TResult, TCreate, TEdit, TKey> :
    ApplicationCreateClient<TResult, TCreate, TKey>,
    IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
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
    public virtual async Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto)
        => await RequestAsync<TResult>(HttpMethod.Put, $"/{id}", editDto);
}

/// <summary>
/// 条件查、增、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateEditClient<TResult, TQuery, TCreate, TEdit, TKey> :
    ApplicationQueryCreateClient<TResult, TQuery, TCreate, TKey>,
    IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
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
    public virtual async Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto)
        => await RequestAsync<TResult>(HttpMethod.Put, $"/{id}", editDto);
}