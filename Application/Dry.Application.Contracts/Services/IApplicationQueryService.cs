using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 查询应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery> :
        IApplicationService<TResult>
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<bool> AnyAsync([NotNull] TQuery queryDto);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<int> CountAsync([NotNull] TQuery queryDto);

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync([NotNull] TQuery queryDto);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<TResult[]> ArrayAsync([NotNull] TQuery queryDto);

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<TResult>> PagedArrayAsync([NotNull] PagedQueryDto<TQuery> queryDto);
    }

    /// <summary>
    /// 查增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery, TCreate> :
        IApplicationQueryService<TResult, TQuery>,
        IApplicationService<TResult, TCreate>
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
    public interface IApplicationQueryService<TResult, TQuery, TCreate, TKey> :
        IApplicationQueryService<TResult, TQuery, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
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
    public interface IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey> :
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}