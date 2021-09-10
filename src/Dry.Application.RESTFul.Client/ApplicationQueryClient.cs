using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using Dry.Core.Model;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Client
{
    /// <summary>
    /// 查询应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    public abstract class ApplicationQueryClient<TResult, TQuery> :
        ApiClient,
        IApplicationQueryService<TResult, TQuery>
        where TResult : IResultDto
        where TQuery : IQueryDto
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(TQuery queryDto)
        {
            return await RequestAsync<bool>(HttpMethod.Get, "/Any", queryDto);
        }

        /// <summary>
        /// 数量查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(TQuery queryDto)
        {
            return await RequestAsync<int>(HttpMethod.Get, "/Count", queryDto);
        }

        /// <summary>
        /// 条件查询第一条
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult> FirstAsync(TQuery queryDto)
        {
            return await RequestAsync<TResult>(HttpMethod.Get, "/First", queryDto);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<TResult[]> ArrayAsync(TQuery queryDto)
        {
            return await RequestAsync<TResult[]>(HttpMethod.Get, null, queryDto);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        public virtual async Task<PagedResult<TResult>> ArrayAsync([NotNull] PagedQuery<TQuery> queryDto)
        {
            return await RequestAsync<PagedResult<TResult>>(HttpMethod.Get, "/Paged", queryDto);
        }
    }

    /// <summary>
    /// 查增应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    public abstract class ApplicationQueryClient<TResult, TQuery, TCreate> :
        ApplicationQueryClient<TResult, TQuery>,
        IApplicationQueryService<TResult, TQuery, TCreate>
        where TResult : IResultDto
        where TQuery : IQueryDto
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
    /// 查增删应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryClient<TResult, TQuery, TCreate, TKey> :
        ApplicationQueryClient<TResult, TQuery, TCreate>,
        IApplicationQueryService<TResult, TQuery, TCreate, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
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
    /// 查增删改应用访问客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TEdit"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryClient<TResult, TQuery, TCreate, TEdit, TKey> :
        ApplicationQueryClient<TResult, TQuery, TCreate, TKey>,
        IApplicationQueryService<TResult, TQuery, TCreate, TEdit, TKey>
        where TResult : IResultDto
        where TQuery : IQueryDto
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