using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IApplicationService<TResult> where TResult : IResultDto
    {
        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<TResult[]> ArrayAsync();

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PagedResultDto<TResult>> PagedArrayAsync([NotNull] PagedQueryDto query);
    }

    /// <summary>
    /// 应用服务增删改接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    public interface IApplicationService<TResult, TCreate, TEdit> :
        IApplicationService<TResult>,
        IApplicationCreateService<TResult, TCreate>,
        IApplicationEditService<TResult, TEdit>,
        IApplicationDeleteService<TResult>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }

    /// <summary>
    /// 应用服务增删查改接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    public interface IApplicationService<TResult, TQuery, TCreate, TEdit> :
        IApplicationQueryService<TResult, TQuery>,
        IApplicationCreateService<TResult, TCreate>,
        IApplicationEditService<TResult, TEdit>,
        IApplicationDeleteService<TResult>
        where TResult : IResultDto
        where TQuery : IQueryDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}