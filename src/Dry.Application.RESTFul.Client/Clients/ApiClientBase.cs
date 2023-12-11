namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// api客户端
/// </summary>
public abstract class ApiClientBase
{
    /// <summary>
    /// 客户端请求配置器
    /// </summary>
    protected abstract IClientRequestConfigurer RequestConfigurer { get; }

    /// <summary>
    /// 接口相对地址
    /// </summary>
    protected abstract string ApiRelativeUrl { get; }

    /// <summary>
    /// 服务生成器
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApiClientBase(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 创建http请求
    /// </summary>
    /// <param name="method"></param>
    /// <param name="apiPath"></param>
    /// <param name="param"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    protected virtual async Task<HttpRequester> CreateRequester(HttpMethod method, string? apiPath = null, object? param = null, string? paramName = null)
    {
        var serviceUrl = RequestConfigurer.GetServiceUrl();
        var requester = new HttpRequester(method, serviceUrl + ApiRelativeUrl + apiPath);
        await RequestConfigurer.ConfigureAsync(_serviceProvider, requester);
        requester.SetRequestParam(param, paramName);
        return requester;
    }

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
        using var requester = await CreateRequester(method, apiPath, param, paramName);
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
        using var requester = await CreateRequester(method, apiPath, param, paramName);
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