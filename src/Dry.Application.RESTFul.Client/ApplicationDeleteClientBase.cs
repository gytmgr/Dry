namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationDeleteClientBase<TResult, TKey> :
    ApplicationClientBase<TResult, TKey>,
    IApplicationDeleteService<TResult, TKey>
    where TResult : IResultDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationDeleteClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
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
/// 条件查、删客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryDeleteClientBase<TResult, TQuery, TKey> :
    ApplicationQueryClientBase<TResult, TQuery, TKey>,
    IApplicationQueryDeleteService<TResult, TQuery, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryDeleteClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TResult> DeleteAsync(TKey id)
        => (await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}"))!;
}