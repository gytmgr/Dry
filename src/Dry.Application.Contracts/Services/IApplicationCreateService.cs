using Dry.Application.Contracts.Dtos;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 基础查、增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationCreateService<TResult, TCreate> :
        IApplicationService<TResult>,
        ICreateService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 基础查、增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationCreateService<TResult, TCreate, TKey> :
        IApplicationService<TResult, TKey>,
        IApplicationCreateService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 条件查、增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationQueryCreateService<TResult, TQuery, TCreate> :
        IApplicationQueryService<TResult, TQuery>,
        ICreateService<TResult, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 条件查、增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey> :
        IApplicationQueryService<TResult, TQuery, TKey>,
        IApplicationQueryCreateService<TResult, TQuery, TCreate>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
    { }
}