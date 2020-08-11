using Dry.Core.Model;
using Dry.Core.Utilities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dry.Http.Client
{
    /// <summary>
    /// http接口客户端
    /// </summary>
    public static class ApiClient
    {
        /// <summary>
        /// http异步请求
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Result<HttpStatusCode, TData>> RequestAsync<TData>(HttpMethod method, string url, object param = null)
        {
            if (method == HttpMethod.Head || method == HttpMethod.Get || method == HttpMethod.Delete)
            {
                if (param != null)
                {
                    var urlParam = UrlHelper.ObjectToUriParam(param);
                    if (url.Contains("?"))
                    {
                        url = $"{url}&{urlParam}";
                    }
                    else
                    {
                        url = $"{url}?{urlParam}";
                    }
                }
            }
            using var requester = new HttpRequester(method, url);
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (param != null)
                {
                    var strContent = param is string ? (string)param : JsonConvert.SerializeObject(param);
                    requester.Content = new StringContent(strContent, Encoding.UTF8, "application/json");
                }
            }
            var result = await requester.GetStringResult();
            if (result.Code == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<TData>(result.Data);
                return Result<HttpStatusCode, TData>.Create(result.Code, data);
            }
            return Result<HttpStatusCode, TData>.Create(result.Code, result.Data, default);
        }
    }
}