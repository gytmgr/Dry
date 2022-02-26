using Dry.Application.Contracts.Dtos;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 基础查、增、改应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationCreateEditService<TResult, TCreate, TEdit, TKey> :
        IApplicationCreateService<TResult, TCreate, TKey>,
        IApplicationEditService<TResult, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }

    /// <summary>
    /// 条件查、增、改应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryCreateEditService<TResult, TQuery, TCreate, TEdit, TKey> :
        IApplicationQueryCreateService<TResult, TQuery, TCreate, TKey>,
        IApplicationQueryEditService<TResult, TQuery, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}