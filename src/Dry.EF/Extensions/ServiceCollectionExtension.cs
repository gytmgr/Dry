global using Dry.Core.Utilities;
global using Dry.Dependency;
global using Dry.Domain;
global using Dry.Domain.Entities;
global using Dry.Domain.Entities.ValueObjects;
global using Dry.Domain.Queryables;
global using Dry.Domain.Repositories;
global using Dry.EF.EntityConfigs;
global using Dry.EF.Extensions;
global using Dry.EF.Repositories;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.EntityFrameworkCore.Query;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel;
global using System.Diagnostics.CodeAnalysis;
global using System.Linq.Expressions;
global using System.Reflection;

namespace Dry.EF.Extensions;

/// <summary>
/// IOC注入扩展
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 添加持久层注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddEF(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}