namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// 基础查、增、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateDeleteClientBase<TResult, TCreate, TKey> :
    ApplicationCreateClientBase<TResult, TCreate, TKey>,
    IApplicationCreateDeleteService<TResult, TCreate, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateDeleteClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
        => (await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}"))!;
}

/// <summary>
/// 条件查、增、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateDeleteClientBase<TResult, TQuery, TCreate, TKey> :
    ApplicationQueryCreateClientBase<TResult, TQuery, TCreate, TKey>,
    IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateDeleteClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
        => (await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}"))!;
}