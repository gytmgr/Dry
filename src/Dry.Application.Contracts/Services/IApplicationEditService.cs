namespace Dry.Application.Contracts.Services;

/// <summary>
/// 基础查、改应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationEditService<TResult, TEdit, TKey> :
    IApplicationService<TResult, TKey>,
    IEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TEdit : IEditDto
{ }

/// <summary>
/// 条件查、改应用服务接口
/// </summary>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TEdit"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IApplicationQueryEditService<TResult, TQuery, TEdit, TKey> :
    IApplicationQueryService<TResult, TQuery, TKey>,
    IEditService<TResult, TEdit, TKey>
    where TResult : IResultDto
    where TQuery : IQueryDto
    where TEdit : IEditDto
{ }