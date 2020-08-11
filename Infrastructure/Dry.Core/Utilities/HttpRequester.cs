using Dry.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Dry.Core.Utilities
{
    /// <summary>
    /// http请求
    /// </summary>
    public class HttpRequester : IDisposable
    {
        /// <summary>
        /// http方法
        /// </summary>
        private readonly HttpMethod _method;

        /// <summary>
        /// 请求地址
        /// </summary>
        private readonly string _uriString;

        /// <summary>
        /// http头
        /// </summary>
        public ICollection<KeyValuePair<string, string>> Headers { get; set; }

        /// <summary>
        /// http内容
        /// </summary>
        public HttpContent Content { get; set; }

        /// <summary>
        /// http客户端
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// http版本（默认1.1）
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uriString"></param>
        public HttpRequester(HttpMethod method, string uriString)
        {
            _method = method;
            _uriString = uriString;
        }

        /// <summary>
        /// https验证处理（忽略）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetResult()
        {
            using var request = new HttpRequestMessage(_method, new Uri(_uriString));
            if (_uriString.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }
            if (Headers != null)
            {
                foreach (var item in Headers.GroupBy(x => x.Key))
                {
                    if (item.Count() > 1)
                    {
                        request.Headers.Add(item.Key, item.ElementAt(0).Value);
                    }
                    else
                    {
                        request.Headers.Add(item.Key, item.Select(x => x.Value));
                    }
                }
            }
            if (Content != null)
            {
                request.Content = Content;
            }
            if (Client == null)
            {
                Client = new HttpClient();
            }
            return await Client.SendAsync(request);
        }

        /// <summary>
        /// 获取字符串结果
        /// </summary>
        /// <returns></returns>
        public async Task<Result<HttpStatusCode, string>> GetStringResult()
        {
            using var httpResponseMessage = await GetResult();
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            return Result<HttpStatusCode, string>.Create(httpResponseMessage.StatusCode, result);
        }

        /// <summary>
        /// 获取字节结果
        /// </summary>
        /// <returns></returns>
        public async Task<Result<HttpStatusCode, byte[]>> GetByteResult()
        {
            using var httpResponseMessage = await GetResult();
            var result = await httpResponseMessage.Content.ReadAsByteArrayAsync();
            return Result<HttpStatusCode, byte[]>.Create(httpResponseMessage.StatusCode, result);
        }

        /// <summary>
        /// 获取流结果
        /// </summary>
        /// <returns></returns>
        public async Task<Result<HttpStatusCode, Stream>> GetStreamResult()
        {
            using var httpResponseMessage = await GetResult();
            var result = await httpResponseMessage.Content.ReadAsStreamAsync();
            return Result<HttpStatusCode, Stream>.Create(httpResponseMessage.StatusCode, result);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Client?.Dispose();
            Content?.Dispose();
        }
    }
}