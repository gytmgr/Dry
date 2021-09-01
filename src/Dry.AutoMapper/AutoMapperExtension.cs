using AutoMapper;
using Dry.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dry.AutoMapper
{
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
            var prefixList = new List<string> { "Dry." };
            if (prefixs is not null)
            {
                prefixList = prefixList.Union(prefixs).ToList();
            }
            var profileAssemblies = DependencyContext.Default.RuntimeLibraries
                .Where(x => prefixList.Any(y => x.Name.StartsWith(y)))
                .Select(x =>
                {
                    try
                    {
                        return Assembly.Load(new AssemblyName(x.Name));
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(x => x is not null)
                .Where(x => x.GetTypes().Any(y => y.IsDerivedFrom(typeof(Profile))))
                .ToArray();
            services.AddAutoMapper(profileAssemblies);
            return services;
        }
    }
}