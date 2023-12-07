namespace Dry.Application.RESTFul.Client.Clients;

/// <summary>
/// api客户端
/// </summary>
public abstract class ApiClientBase
{
    /// <summary>
    /// 租户id键
    /// </summary>
    protected const string _tenantIdKey = "TenantId";

    /// <summary>
    /// 服务生成器
    /// </summary>
    protected readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 接口地址
    /// </summary>
    protected abstract string ApiUrl { get; }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApiClientBase(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// http请求参数设置
    /// </summary>
    /// <param name="requester"></param>
    /// <param name="param"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    protected virtual async Task RequestParamSettingAsync(HttpRequester requester, object? param = null, string? paramName = null)
    {
        var tenant = _serviceProvider.GetRequiredService<ITenantProvider>();
        if (tenant.Id is not null)
        {
            requester.Headers = new Collection<KeyValuePair<string, string>>();
            requester.Headers.Add(new KeyValuePair<string, string>(_tenantIdKey, tenant.Id));
        }
        requester.SetRequestParam(param, paramName);
        await _serviceProvider.ServicesActionAsync<IClientRequestConfigurer>(async service => await service.ConfigureAsync(_serviceProvider, requester, param, paramName));
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
        using var requester = new HttpRequester(method, ApiUrl + apiPath);
        await RequestParamSettingAsync(requester, param, paramName);
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
        await RequestParamSettingAsync(requester, param, paramName);
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