using Dry.Application.Contracts.Dtos;
using Dry.Application.Contracts.Services;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Client
{
    /// <summary>
    /// 基础查、增、删客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationCreateDeleteClient<TResult, TCreate, TKey> :
        ApplicationCreateClient<TResult, TCreate, TKey>,
        IApplicationCreateDeleteService<TResult, TCreate, TKey>
        where TResult : IResultDto
        where TCreate : ICreateDto
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
    /// 条件查、增、删客户端
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TCreate"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ApplicationQueryCreateDeleteClient<TResult, TQuery, TCreate, TKey> :
        ApplicationQueryCreateClient<TResult, TQuery, TCreate, TKey>,
        IApplicationQueryCreateDeleteService<TResult, TQuery, TCreate, TKey>
        where TResult : IResultDto
        where TQuery : QueryDto<TKey>
        where TCreate : ICreateDto
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