using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 应用服务条件查询接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public interface IApplicationQueryService<TResult, TQuery> : IApplicationService<TResult> where TResult : IResultDto where TQuery : IQueryDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<bool> AnyAsync([NotNull] TQuery query);

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<int> CountAsync([NotNull] TQuery query);

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult> FirstAsync([NotNull] TQuery query);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TResult[]> ArrayAsync([NotNull] TQuery query);

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PagedResultDto<TResult>> PagedArrayAsync([NotNull] PagedQueryDto<TQuery> query);
    }
}