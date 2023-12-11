global using Dry.Application.Contracts.Dtos;
global using Dry.Application.Contracts.Services;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using Microsoft.Extensions.DependencyInjection;
global using System.Collections.ObjectModel;
global using System.Net;

namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 客户端请求配置器接口
/// </summary>
public interface IClientRequestConfigurer
{
    /// <summary>
    /// 租户id键
    /// </summary>
    protected static string _tenantIdKey = "TenantId";

    /// <summary>
    /// 获取服务地址
    /// </summary>
    /// <returns></returns>
    string GetServiceUrl();

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="requester"></param>
    /// <returns></returns>
    public Task ConfigureAsync(IServiceProvider serviceProvider, HttpRequester requester)
    {
        var tenant = serviceProvider.GetRequiredService<ITenantProvider>();
        if (tenant.Id is not null)
        {
            requester.Headers = new Collection<KeyValuePair<string, string>>();
            requester.Headers.Add(new KeyValuePair<string, string>(_tenantIdKey, tenant.Id));
        }
        return Task.CompletedTask;
    }
}