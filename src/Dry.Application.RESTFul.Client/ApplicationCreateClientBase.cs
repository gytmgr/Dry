namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 基础查、增客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationCreateClientBase<TResult, TCreate> :
    ApplicationClientBase<TResult>,
    IApplicationCreateService<TResult, TCreate>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
        => (await RequestAsync<TResult>(HttpMethod.Post, null, createDto))!;
}

/// <summary>
/// 基础查、增客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationCreateClientBase<TResult, TCreate, TKey> :
    ApplicationClientBase<TResult, TKey>,
    IApplicationCreateService<TResult, TCreate, TKey>
    where TResult : IResultDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationCreateClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
        => (await RequestAsync<TResult>(HttpMethod.Post, null, createDto))!;
}

/// <summary>
/// 条件查、增客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
public abstract class ApplicationQueryCreateClientBase<TResult, TQuery, TCreate> :
    ApplicationQueryClientBase<TResult, TQuery>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
        => (await RequestAsync<TResult>(HttpMethod.Post, null, createDto))!;
}

/// <summary>
/// 条件查、增客户端
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TCreate"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class ApplicationQueryCreateClientBase<TResult, TQuery, TCreate, TKey> :
    ApplicationQueryClientBase<TResult, TQuery, TKey>,
    IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>
    where TResult : IResultDto
    where TQuery : QueryDto<TKey>
    where TCreate : ICreateDto
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApplicationQueryCreateClientBase(IServiceProvider serviceProvider) : base(serviceProvider)
    { }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns></returns>
    public virtual async Task<TResult> CreateAsync(TCreate createDto)
        => (await RequestAsync<TResult>(HttpMethod.Post, null, createDto))!;
}