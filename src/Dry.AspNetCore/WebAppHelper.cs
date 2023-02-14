global using Dry.AspNetCore.Infrastructure.AppBuilderConfigure;
global using Dry.AspNetCore.Infrastructure.AppConfigure;
global using Dry.AspNetCore.Resources;
global using Dry.Core.Model;
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
    public static async Task<WebApplication> CreateAsync(string[] args, params string[] dependencyPrefixs)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDependency(true, dependencyPrefixs);

        var serviceProvider = builder.Services.BuildServiceProvider();

        await ServicesActionAsync<IAppBuilderConfigurer>(serviceProvider, async service => await service.ConfigureAsync(builder));

        var app = builder.Build();

        await ServicesActionAsync<IAppConfigurer>(serviceProvider, async service => await service.ConfigureAsync(app));

        return app;
    }

    /// <summary>
    /// 多服务操作
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceAction"></param>
    /// <param name="ascOrder"></param>
    /// <returns></returns>
    public static async Task ServicesActionAsync<TService>(IServiceProvider serviceProvider, Func<TService, Task> serviceAction, bool ascOrder = true)
    {
        var executers = serviceProvider.GetServices<TService>();
        if (typeof(IHasOrder).IsAssignableFrom(typeof(TService)))
        {
            executers = ascOrder ? executers.OrderBy(x => ((IHasOrder)x).Order) : executers.OrderByDescending(x => ((IHasOrder)x).Order);
        }
        foreach (var executer in executers)
        {
            await serviceAction(executer);
        }
    }
}