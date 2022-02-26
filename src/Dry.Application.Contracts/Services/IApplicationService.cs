using Dry.Application.Contracts.Dtos;
using Dry.Core.Model;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 基础查询应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IApplicationService<TResult>
        where TResult : IResultDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <returns></returns>
        Task<TResult> FirstAsync();

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<TResult[]> ArrayAsync();

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery queryDto);
    }

    /// <summary>
    /// 基础查询应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TKey> :
        IApplicationService<TResult>,
        IFindService<TResult, TKey>
        where TResult : IResultDto
    {
    }

    /// <summary>
    /// 基础查、增、改、删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TCreate, TEdit, TKey> :
        IApplicationCreateEditService<TResult, TCreate, TEdit, TKey>,
        IApplicationCreateDeleteService<TResult, TCreate, TKey>,
        IApplicationEditDeleteService<TResult, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}