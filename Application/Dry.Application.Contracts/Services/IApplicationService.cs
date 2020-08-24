using Dry.Application.Contracts.Dtos;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Dry.Application.Contracts.Services
{
    /// <summary>
    /// 应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IApplicationService<TResult>
        where TResult : IResultDto
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
        Task<PagedResultDto<TResult>> PagedArrayAsync([NotNull] PagedQueryDto queryDto);
    }

    /// <summary>
    /// 新增应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public interface IApplicationService<TResult, TCreate> :
        IApplicationService<TResult>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        Task<TResult> CreateAsync([NotNull] TCreate createDto);
    }

    /// <summary>
    /// 增删应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TCreate, TKey>
        : IApplicationService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResult> FindAsync([NotNull] TKey id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TResult> DeleteAsync([NotNull] TKey id);
    }

    /// <summary>
    /// 增删改应用服务接口
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationService<TResult, TCreate, TEdit, TKey> :
        IApplicationService<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
        where TEdit : IEditDto
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editDto"></param>
        /// <returns></returns>
        Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto);
    }
}