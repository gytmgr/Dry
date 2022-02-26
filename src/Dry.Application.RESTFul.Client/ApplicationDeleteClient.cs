using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Client
{
    /// <summary>
    /// 基础查、删客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationDeleteClient<TResult, TKey> :
        ApplicationClient<TResult, TKey>,
        IApplicationDeleteService<TResult, TKey>
        where TResult : IResultDto
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
            => await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
    }

    /// <summary>
    /// 条件查、删客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryDeleteClient<TResult, TQuery, TKey> :
        ApplicationQueryClient<TResult, TQuery, TKey>,
        IApplicationQueryDeleteService<TResult, TQuery, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TResult> DeleteAsync([NotNull] TKey id)
            => await RequestAsync<TResult>(HttpMethod.Delete, $"/{id}");
    }
}