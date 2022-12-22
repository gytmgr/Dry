namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查、改、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditDeleteClient<TResult, TEdit, TKey> :
    ApplicationEditClient<TResult, TEdit, TKey>,
    IApplicationEditDeleteService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
        => await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
}

/// <summary>
/// 条件查、改、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditDeleteClient<TResult, TQuery, TEdit, TKey> :
    ApplicationQueryEditClient<TResult, TQuery, TEdit, TKey>,
    IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TEdit : IEditDto
{
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
        => await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
}