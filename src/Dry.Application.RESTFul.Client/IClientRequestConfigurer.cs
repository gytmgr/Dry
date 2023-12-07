global using Dry.Application.Contracts.Dtos;
global using Dry.Application.Contracts.Services;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using Dry.Dependency;
global using Microsoft.Extensions.DependencyInjection;
global using System.Collections.ObjectModel;
global using System.Net;

namespace Dry.Application.RESTFul.Client;

/// <summary>
/// 客户端请求配置器接口
/// </summary>
public interface IClientRequestConfigurer : ISingletonDependency<IClientRequestConfigurer>
{
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="requester"></param>
    /// <param name="param"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    Task ConfigureAsync(IServiceProvider provider, HttpRequester requester, object? param = null, string? paramName = null);
}