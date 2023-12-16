global using Dry.Admin.Application.Contracts.Dtos;
global using Dry.Admin.Application.Contracts.Services;
global using Dry.Application.RESTFul.Client;
global using Dry.Application.RESTFul.Client.Clients;
global using Dry.Core.Model;
global using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.Application.RESTFul.Client;

public class ClientRequestConfigurer : IClientRequestConfigurer, ISingletonDependency<ClientRequestConfigurer>
{
    /// <summary>
    /// 接口地址
    /// </summary>
    public string ApiUrl { get; set; }

    public string GetServiceUrl()
        => ApiUrl;
}