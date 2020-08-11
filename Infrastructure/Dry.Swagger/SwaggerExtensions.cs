using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

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
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Supermarket API",
                    Version = "v3",
                    Description = "Simple RESTful API built with ASP.NET Core 3.1 to show how to create RESTful services using a decoupled, maintainable architecture.",
                    Contact = new OpenApiContact
                    {
                        Name = "Evandro Gayer Gomes",
                        Url = new Uri("https://evandroggomes.com.br/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    },
                });

                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
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
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API");
                options.DocumentTitle = "Supermarket API";
            });
            return app;
        }
    }
}