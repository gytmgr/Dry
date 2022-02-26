using Dry.Application.Contracts.Dtos;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 基础查、增、删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationCreateDeleteService<TResult, TCreate, TKey> :
        IApplicationCreateService<TResult, TCreate, TKey>,
        IApplicationDeleteService<TResult, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 条件查、增、删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey> :
        IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>,
        IApplicationQueryDeleteService<TResult, TQuery, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
    { }
}