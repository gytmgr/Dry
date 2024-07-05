namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// 基础查、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationEditClientBase<TResult, TEdit, TKey> :
    ApplicationClientBase<TResult, TKey>,
    IApplicationEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationEditClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> EditAsync(TKey id, TEdit editDto)
        => (await RequestAsync<TResult>(HttpMethod.Put, $"/{id}", editDto))!;
}

/// <summary>
/// 条件查、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryEditClientBase<TResult, TQuery, TEdit, TKey> :
    ApplicationQueryClientBase<TResult, TQuery, TKey>,
    IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryEditClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="editDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> EditAsync(TKey id, TEdit editDto)
        => (await RequestAsync<TResult>(HttpMethod.Put, $"/{id}", editDto))!;
}