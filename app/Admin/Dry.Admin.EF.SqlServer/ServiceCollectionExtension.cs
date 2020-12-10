using Dry.Admin.Domain;
using Dry.Admin.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dry.Admin.EF.SqlServer
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdminSqlServerEFContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IAdminContext, AdminDbContext>(options =>
            {
                options.UseSqlServer(connectionString, x =>
                {
                    x.CommandTimeout(120);
                    x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });
            });
            return services;
        }
    }
}