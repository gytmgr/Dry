namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditClient<TResult, TEdit, TKey> :
    ApplicationClient<TResult, TKey>,
    IApplicationEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
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
/// 条件查、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditClient<TResult, TQuery, TEdit, TKey> :
    ApplicationQueryClient<TResult, TQuery, TKey>,
    IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
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