﻿global using Dry.Admin.Domain;
global using Dry.Admin.EF.Contexts;
global using Dry.Dependency;
global using Dry.EF.Extensions;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;

namespace Dry.Admin.EF.SqlServer;

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