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