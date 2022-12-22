global using AutoMapper;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using Dry.Dependency;
global using Microsoft.Extensions.DependencyInjection;

namespace Dry.AutoMapper;

/// <summary>
/// AutoMapper扩展
/// </summary>
public static class AutoMapperExtension
{
    /// <summary>
    /// 添加AutoMapper扩展服务注册
    /// </summary>
    /// <param name="services"></param>
    /// <param name="prefixs">程序集名称前缀</param>
    /// <returns></returns>
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services, params string[] prefixs)
    {
        var prefixList = new string[] { "Dry." };
        if (prefixs is not null)
        {
            prefixList = prefixList.Union(prefixs).ToArray();
        }
        var profileAssemblies = AssemblyHelper.GetAll(prefixList)
            .Where(x => x is not null)
            .Where(x => x.GetTypes().Any(y => typeof(Profile).IsAssignableFrom(y)))
            .ToArray();
        services.AddAutoMapper(profileAssemblies);
        return services;
    }
}