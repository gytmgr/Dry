using Dry.Admin.Application.Contracts.Services;
using Dry.Admin.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdminApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IApplicationAppService), typeof(ApplicationAppService));

            return services;
        }
    }
}