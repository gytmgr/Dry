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
        /// <param name="queryDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<TResult>> ArrayAsync([NotNull] PagedQueryDto queryDto);
    }

    /// <summary>
    /// 新增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationService<TResult, TCreate> : IApplicationService<TResult>, IApplicationCreateService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 增删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TCreate, TKey> : IApplicationService<TResult, TCreate>, IApplicationDeleteService<TResult, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
    { }

    /// <summary>
    /// 增删改应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TCreate, TEdit, TKey> : IApplicationService<TResult, TCreate, TKey>, IApplicationEditService<TResult, TEdit, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    { }
}