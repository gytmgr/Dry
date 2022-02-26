using Dry.Application.Contracts.Dtos;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 基础查、改、删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationEditDeleteService<TResult, TEdit, TKey> :
        IApplicationEditService<TResult, TEdit, TKey>,
        IApplicationDeleteService<TResult, TKey>
        where TResult : IResultDto
        where TEdit : IEditDto
    { }

    /// <summary>
    /// 条件查、改、删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryEditDeleteService<TResult, TQuery, TEdit, TKey> :
        IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>,
        IApplicationQueryDeleteService<TResult, TQuery, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TEdit : IEditDto
    { }
}