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
    /// http请求扩展
    /// </summary>
    public static class HttpRequesterExtension
    {
        /// <summary>
        /// 返回泛型结果
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="requester"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Result<HttpStatusCode, TData>> GetResultAsync<TData>(this HttpRequester requester, object param = null)
        {
            if (requester.Method == HttpMethod.Head || requester.Method == HttpMethod.Get || requester.Method == HttpMethod.Delete)
            {
                if (param != null)
                {
                    var urlParam = UrlHelper.ObjectToUriParam(param);
                    if (requester.Url.Contains("?"))
                    {
                        requester.Url = $"{requester.Url}&{urlParam}";
                    }
                    else
                    {
                        requester.Url = $"{requester.Url}?{urlParam}";
                    }
                }
            }
            else if (requester.Method == HttpMethod.Post || requester.Method == HttpMethod.Put)
            {
                if (param != null)
                {
                    var strContent = param is string strParam ? strParam : JsonConvert.SerializeObject(param);
                    requester.Content = new StringContent(strContent, Encoding.UTF8, "application/json");
                }
            }
            var result = await requester.GetStringResultAsync();
            if (result.Code == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<TData>(result.Data);
                return Result<HttpStatusCode, TData>.Create(result.Code, data);
            }
            return Result<HttpStatusCode, TData>.Create(result.Code, result.Data, default);
        }
    }
}