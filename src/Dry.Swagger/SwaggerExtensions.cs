using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
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
        /// 基础配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="title"></param>
        private static void BasicConfig(SwaggerGenOptions options, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Dry API";
            }

            options.CustomSchemaIds(type => type.ToString());

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = title,
                Version = "v1",
            });

            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
            {
                options.IncludeXmlComments(file, true);
            });
        }

        /// <summary>
        /// 添加Swagger服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string title = null)
        {
            services.AddSwaggerGen(x => BasicConfig(x, title));
            return services;
        }

        /// <summary>
        /// 添加基于jwt认证的Swagger服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtSwagger(this IServiceCollection services, string title = null)
        {
            services.AddSwaggerGen(x =>
            {
                BasicConfig(x, title);

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "请输入Token",
                    BearerFormat = "JWT"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
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