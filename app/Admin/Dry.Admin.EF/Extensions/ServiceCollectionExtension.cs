using Dry.Admin.Domain;
using Dry.Admin.EF.EntityConfigs;
using Dry.EF.EntityConfigs;
using Dry.EF.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Dry.Admin.EF.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdminEF(this IServiceCollection services)
        {
            services.AddEF();

            services.AddScoped(typeof(IEntityRegister<IAdminContext>), typeof(ApplicationConfig));
            services.AddScoped(typeof(IEntityRegister<IAdminContext>), typeof(ResourceConfig));
            services.AddScoped(typeof(IEntityRegister<IAdminContext>), typeof(ResourceItemConfig));

            return services;
        }
    }
}