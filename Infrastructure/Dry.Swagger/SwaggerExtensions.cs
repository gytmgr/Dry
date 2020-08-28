using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;

namespace Dry.Swagger
{
    /// <summary>
    /// Swagger中间件扩展
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 添加Swagger服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string title = null)
        {
            if (title == null)
            {
                title = "Blue API";
            }
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = title,
                    Version = "v1",
                });

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    cfg.IncludeXmlComments(file, true);
                });
            });
            return services;
        }
        /// <summary>
        /// 使用Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blue API");
                options.DocumentTitle = "Supermarket API";
            });
            return app;
        }
    }
}