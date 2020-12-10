using Dry.Admin.Domain;
using Dry.Admin.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dry.Admin.EF.Sqlite
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAdminSqliteEFContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IAdminContext, AdminDbContext>(options =>
            {
                options.UseSqlite(connectionString, x =>
                {
                    x.CommandTimeout(120);
                    x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });
            });
            return services;
        }
    }
}