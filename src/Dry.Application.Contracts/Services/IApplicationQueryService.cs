using Dry.Application.Contracts.Dtos;
using Dry.Core.Model;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 查询应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(TQuery queryDto = default);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<int> CountAsync(TQuery queryDto = default);

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync(TQuery queryDto = default);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<TResult[]> ArrayAsync(TQuery queryDto = default);

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery<TQuery> queryDto);
    }

    /// <summary>
    /// 查增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery, TCreate> : IApplicationQueryService<TResult, TQuery>, IApplicationCreateService<TResult, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 查增删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery, TCreate, TKey> : IApplicationQueryService<TResult, TQuery, TCreate>, IApplicationDeleteService<TResult, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 查增删改应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> : IApplicationQueryService<TResult, TQuery, TCreate, TKey>, IApplicationEditService<TResult, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}