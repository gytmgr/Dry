namespace Dry.Application.Contracts.Services;

/// <summary>
/// 基础查、删应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationDeleteService<TResult, TKey> :
    IApplicationService<TResult, TKey>,
    IDeleteService<TResult, TKey>
    where TResult : IResultDto
{ }

/// <summary>
/// 条件查、删应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationQueryDeleteService<TResult, TQuery, TKey> :
    IApplicationQueryService<TResult, TQuery, TKey>,
    IDeleteService<TResult, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
{ }