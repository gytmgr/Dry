using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Client
{
    /// <summary>
    /// 应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ApplicationClient<TResult> :
        ApiClient,
        IApplicationService<TResult>
        where TResult : IResultDto
    {
        /// <summary>
        /// 数量查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await RequestAsync<int>(HttpMethod.Get, "/Count");
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync()
        {
            return await RequestAsync<TResult[]>(HttpMethod.Get);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery queryDto)
        {
            return await RequestAsync<PagedResult<TResult>>(HttpMethod.Get, "/Paged", queryDto);
        }
    }

    /// <summary>
    /// 新增应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationClient<TResult, TCreate> :
        ApplicationClient<TResult>,
        IApplicationService<TResult, TCreate>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> CreateAsync([NotNull] TCreate createDto)
        {
            return await RequestAsync<TResult>(HttpMethod.Post, null, createDto);
        }
    }

    /// <summary>
    /// 增删应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationClient<TResult, TCreate, TKey> :
        ApplicationClient<TResult, TCreate>,
        IApplicationService<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
    {
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FindAsync([NotNull] TKey id)
        {
            return await RequestAsync<TResult>(HttpMethod.Get, $"/{id}");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
        {
            return await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
        }
    }

    /// <summary>
    /// 增删改应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationClient<TResult, TCreate, TEdit, TKey> :
        ApplicationClient<TResult, TCreate, TKey>,
        IApplicationService<TResult, TCreate, TEdit, TKey>
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
        public virtual async Task<TResult> EditAsync([NotNull] TKey id, [NotNull] TEdit editDto)
        {
            return await RequestAsync<TResult>(HttpMethod.Put, $"/{id}", editDto);
        }
    }
}