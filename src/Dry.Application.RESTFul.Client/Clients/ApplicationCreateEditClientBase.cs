namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// 基础查、增、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateEditClientBase<TResult, TCreate, TEdit, TKey> :
    ApplicationCreateClientBase<TResult, TCreate, TKey>,
    IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateEditClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
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
/// 条件查、增、改客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateEditClientBase<TResult, TQuery, TCreate, TEdit, TKey> :
    ApplicationQueryCreateClientBase<TResult, TQuery, TCreate, TKey>,
    IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
    where TEdit : IEditDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateEditClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
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