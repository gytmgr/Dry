global using Dry.Application.Contracts.Dtos;
global using Dry.Application.Contracts.Services;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using System.Net;

namespace Dry.Application.RESTFul.Client;

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
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="BizException"></exception>
    /// <exception cref="Exception"></exception>
    protected virtual async Task RequestAsync(HttpMethod method, string? apiPath = null, object? param = null, string? paramName = null)
    {
        using var requester = new HttpRequester(method, ApiUrl + apiPath);
        if (param is not null)
        {
            requester.SetRequestParam(param, paramName);
        }
        var response = await requester.GetStringResultAsync();
        if (response.Code is HttpStatusCode.OK or HttpStatusCode.NoContent)
        {
            return;
        }
        if (response.Code is HttpStatusCode.BadRequest)
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
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="BizException"></exception>
    /// <exception cref="Exception"></exception>
    protected virtual async Task<TData?> RequestAsync<TData>(HttpMethod method, string? apiPath = null, object? param = null, string? paramName = null)
    {
        using var requester = new HttpRequester(method, ApiUrl + apiPath);
        if (param is not null)
        {
            requester.SetRequestParam(param, paramName);
        }
        var response = await requester.GetResultAsync<TData>();
        if (response.Code is HttpStatusCode.OK or HttpStatusCode.NoContent)
        {
            return response.Data;
        }
        if (response.Code is HttpStatusCode.BadRequest)
        {
            throw new BizException(response.Message);
        }
        throw new Exception(response.Message);
    }
}