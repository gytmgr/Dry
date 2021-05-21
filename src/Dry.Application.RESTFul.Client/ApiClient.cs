using Dry.Core.Model;
using Dry.Core.Utilities;
using Dry.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Application.RESTFul.Client
{
    /// <summary>
    /// api客户端
    /// </summary>
    public abstract class ApiClient
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        protected abstract string ApiUrl { get; }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="apiPath"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected virtual async Task RequestAsync(HttpMethod method, string apiPath = null, object param = null)
        {
            using var requester = new HttpRequester(method, ApiUrl + apiPath);
            requester.SetRequestParam(param);
            var response = await requester.GetStringResultAsync();
            if (response.Code == HttpStatusCode.OK || response.Code == HttpStatusCode.NoContent)
            {
                return;
            }
            if (response.Code == HttpStatusCode.BadRequest)
            {
                throw new BizException(response.Data);
            }
            throw new Exception(response.Data);
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="method"></param>
        /// <param name="apiPath"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected virtual async Task<TData> RequestAsync<TData>(HttpMethod method, string apiPath = null, object param = null)
        {
            using var requester = new HttpRequester(method, ApiUrl + apiPath);
            var response = await requester.GetResultAsync<TData>(param);
            if (response.Code == HttpStatusCode.OK || response.Code == HttpStatusCode.NoContent)
            {
                return response.Data;
            }
            if (response.Code == HttpStatusCode.BadRequest)
            {
                throw new BizException(response.Message);
            }
            throw new Exception(response.Message);
        }
    }
}