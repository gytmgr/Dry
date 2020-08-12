using Dry.Core.Model;
using Dry.Core.Utilities;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dry.Http.Client
{
    /// <summary>
    /// http接口信息
    /// </summary>
    public class ApiInfo
    {
        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 补充接口地址格式
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ApiInfo UrlFormat([NotNull] params object[] args)
        {
            Url = string.Format(Url, args);
            return this;
        }

        /// <summary>
        /// http异步请求
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Result<HttpStatusCode, TData>> RequestAsync<TData>(object param = null)
        {
            if (HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Delete)
            {
                if (param != null)
                {
                    var urlParam = UrlHelper.ObjectToUriParam(param);
                    if (Url.Contains("?"))
                    {
                        Url = $"{Url}&{urlParam}";
                    }
                    else
                    {
                        Url = $"{Url}?{urlParam}";
                    }
                }
            }
            using var requester = new HttpRequester(HttpMethod, Url);
            if (HttpMethod == HttpMethod.Post || HttpMethod == HttpMethod.Put)
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