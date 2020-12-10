using Dry.Admin.Application.Contracts.Services;
using Dry.Admin.Application.RESTFul.Client.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.Application.RESTFul.Client.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdminClient(this IServiceCollection services)
        {
            services.AddScoped(typeof(IApplicationAppService), typeof(ApplicationClient));

            return services;
        }
    }
}