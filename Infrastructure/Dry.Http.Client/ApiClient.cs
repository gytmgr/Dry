using Dry.Core.Utilities;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Http.Client
{
    /// <summary>
    /// api客户端
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApiClient<T> where T : new()
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public static T Instance { get { return new T(); } }

        /// <summary>
        /// 接口地址
        /// </summary>
        protected abstract string ApiUrl { get; }

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
            return response.Data;
        }
    }
}