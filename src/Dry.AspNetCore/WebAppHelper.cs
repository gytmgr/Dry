global using Dry.AspNetCore.Infrastructure.AppBuilderConfigure;
global using Dry.AspNetCore.Infrastructure.AppConfigure;
global using Dry.AspNetCore.Resources;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using Dry.Dependency;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

namespace Dry.AspNetCore;

/// <summary>
/// web应用帮助类
/// </summary>
public static class WebAppHelper
{
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="args"></param>
    /// <param name="dependencyPrefixs"></param>
    /// <returns></returns>
    public static async Task<WebApplication> CreateAsync(string[] args, params string[]? dependencyPrefixs)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDependency(true, dependencyPrefixs);
        builder.Services.AddHttpClient();

        var provider = builder.Services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        HttpRequester.GetClient = () => scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();

        await scope.ServiceProvider.ServicesActionAsync<IAppBuilderConfigurer>(async service => await service.ConfigureAsync(builder));

        var app = builder.Build();
        HttpRequester.GetClient = () => app.Services.GetRequiredService<IHttpClientFactory>().CreateClient();

        using var appScope = app.Services.CreateScope();
        await appScope.ServiceProvider.ServicesActionAsync<IAppConfigurer>(async service => await service.ConfigureAsync(app));

        return app;
    }
}