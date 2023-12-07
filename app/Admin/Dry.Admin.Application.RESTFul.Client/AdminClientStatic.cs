global using Dry.Admin.Application.Contracts.Dtos;
global using Dry.Admin.Application.Contracts.Services;
global using Dry.Application.RESTFul.Client.Clients;
global using Dry.Core.Model;

namespace Dry.Admin.Application.RESTFul.Client;

public class AdminClientStatic
{
    /// <summary>
    /// 接口地址
    /// </summary>
    public static string ApiUrl { get; set; }
}