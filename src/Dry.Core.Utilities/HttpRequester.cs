namespace Dry.Core.Utilities;

/// <summary>
/// http请求
/// </summary>
public class HttpRequester : IDisposable
{
    /// <summary>
    /// http客户端
    /// </summary>
    private HttpClient? _client;

    /// <summary>
    /// http方法
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// http头
    /// </summary>
    public ICollection<KeyValuePair<string, string>>? Headers { get; set; }

    /// <summary>
    /// http内容
    /// </summary>
    public HttpContent? Content { get; set; }

    /// <summary>
    /// http版本（默认1.1）
    /// </summary>
    public Version? Version { get; set; }

    /// <summary>
    /// 获取http客户端
    /// </summary>
    public static Func<HttpClient>? GetClient { private get; set; }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="method"></param>
    /// <param name="url"></param>
    public HttpRequester(HttpMethod method, string url)
    {
        method.CheckParamNull(nameof(method));
        url.CheckParamNull(nameof(url));

        Method = method;
        Url = url;
    }

    /// <summary>
    /// https验证处理（忽略）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="certificate"></param>
    /// <param name="chain"></param>
    /// <param name="sslPolicyErrors"></param>
    /// <returns></returns>
    private static bool CheckValidationResult(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    /// <summary>
    /// 设置请求参数
    /// </summary>
    /// <param name="param"></param>
    /// <param name="paramName"></param>
    public void SetRequestParam(object param, string? paramName = null)
    {
        if (Method == HttpMethod.Post || Method == HttpMethod.Put || Method == HttpMethod.Patch)
        {
            var strContent = DryJsonSerializer.Serialize(param);
            Content = new StringContent(strContent, Encoding.UTF8, "application/json");
        }
        else if (Method == HttpMethod.Head || Method == HttpMethod.Get || Method == HttpMethod.Delete)
        {
            if (param is not null)
            {
                var urlParam = param.ObjectToUriParam(paramName);
                if (urlParam.Length > 0)
                {
                    if (Url.Contains('?'))
                    {
                        Url = $"{Url}&{urlParam}";
                    }
                    else
                    {
                        Url = $"{Url}?{urlParam}";
                    }
                }
            }
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> GetResultAsync(HttpClient? client = null)
    {
        using var request = new HttpRequestMessage(Method, new Uri(Url));
        if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
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
        client ??= GetClient?.Invoke();
        client ??= _client = new HttpClient();
        return await client.SendAsync(request);
    }

    /// <summary>
    /// 返回泛型结果
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<Result<HttpStatusCode, TData>> GetResultAsync<TData>(HttpClient? client = null)
    {
        var result = await GetStringResultAsync(client);
        if (result is { Code: HttpStatusCode.OK or HttpStatusCode.NoContent, Data: not null and { Length: > 0 } })
        {
            var data = DryJsonSerializer.Deserialize<TData>(result.Data);
            return Result<HttpStatusCode, TData>.Create(result.Code, data);
        }
        return Result<HttpStatusCode, TData>.Create(result.Code, result.Data);
    }

    /// <summary>
    /// 获取字符串结果
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<Result<HttpStatusCode, string>> GetStringResultAsync(HttpClient? client = null)
    {
        using var httpResponseMessage = await GetResultAsync(client);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        return Result<HttpStatusCode, string>.Create(httpResponseMessage.StatusCode, result);
    }

    /// <summary>
    /// 获取字节结果
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<Result<HttpStatusCode, byte[]>> GetByteResult(HttpClient? client = null)
    {
        using var httpResponseMessage = await GetResultAsync(client);
        var result = await httpResponseMessage.Content.ReadAsByteArrayAsync();
        return Result<HttpStatusCode, byte[]>.Create(httpResponseMessage.StatusCode, result);
    }

    /// <summary>
    /// 获取流结果
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<Result<HttpStatusCode, Stream>> GetStreamResult(HttpClient? client = null)
    {
        using var httpResponseMessage = await GetResultAsync(client);
        var result = await httpResponseMessage.Content.ReadAsStreamAsync();
        return Result<HttpStatusCode, Stream>.Create(httpResponseMessage.StatusCode, result);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _client?.Dispose();
        Content?.Dispose();
    }
}