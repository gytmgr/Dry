﻿namespace Dry.Core.Json;

/// <summary>
/// http请求扩展
/// </summary>
public static class HttpRequesterExtension
{
    /// <summary>
    /// 设置请求参数
    /// </summary>
    /// <param name="requester"></param>
    /// <param name="param"></param>
    public static void SetRequestParam(this HttpRequester requester, object param = null)
    {
        if (requester.Method == HttpMethod.Post || requester.Method == HttpMethod.Put)
        {
            var strContent = JsonSerializer.Serialize(param, new JsonSerializerOptions().DefaultConfig());
            requester.Content = new StringContent(strContent, Encoding.UTF8, "application/json");
        }
        else if (requester.Method == HttpMethod.Head || requester.Method == HttpMethod.Get || requester.Method == HttpMethod.Delete)
        {
            if (param is not null)
            {
                var urlParam = param.ObjectToUriParam();
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
    }

    /// <summary>
    /// 返回泛型结果
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="requester"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<Result<HttpStatusCode, TData>> GetResultAsync<TData>(this HttpRequester requester, object param = null)
    {
        requester.SetRequestParam(param);
        var result = await requester.GetStringResultAsync();
        if (result.Code == HttpStatusCode.OK || result.Code == HttpStatusCode.NoContent)
        {
            if (string.IsNullOrEmpty(result.Data))
            {
                return Result<HttpStatusCode, TData>.Create(result.Code, default);
            }
            var data = JsonSerializer.Deserialize<TData>(result.Data, new JsonSerializerOptions().DefaultConfig());
            return Result<HttpStatusCode, TData>.Create(result.Code, data);
        }
        return Result<HttpStatusCode, TData>.Create(result.Code, result.Data, default);
    }
}