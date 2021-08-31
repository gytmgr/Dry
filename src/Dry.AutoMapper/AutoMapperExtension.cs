using AutoMapper;
using Dry.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
        /// <returns></returns>
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            var profileAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetTypes().Any(y => y.IsDerivedFrom(typeof(Profile))));
            services.AddAutoMapper(profileAssemblies);
            return services;
        }
    }
}