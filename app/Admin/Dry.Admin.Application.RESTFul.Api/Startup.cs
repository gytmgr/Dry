using AutoMapper;
using Dry.Admin.Application.Extensions;
using Dry.Admin.Application.Mapping;
using Dry.Admin.EF.Extensions;
using Dry.Admin.EF.Sqlite;
using Dry.Application.Mapping;
using Dry.Mvc.Infrastructure;
using Dry.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dry.Admin.Application.RESTFul.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddCustomSwagger();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConvert());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });
            services.AddCors(options => options.AddPolicy("AllowOrigins", p => p
                .WithOrigins(Configuration.GetSection("AppSettings:FrontEndUrl").Get<string[]>())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            ));

            services.AddAdminSqliteEFContext(Configuration.GetConnectionString("default"));

            services.AddAdminEF();
            services.AddAdminApplication();

            services.AddAutoMapper(typeof(ApplicationProfile));
            services.AddAutoMapper(typeof(ValueProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();

            app.UseRouting();

            app.UseCors("AllowOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}