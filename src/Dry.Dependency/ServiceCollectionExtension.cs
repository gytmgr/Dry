global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyModel;
global using System.Reflection;

namespace Dry.Dependency;

/// <summary>
/// 依赖注入扩展
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 注入实现IDependency接口的类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="prefixs"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependency(this IServiceCollection services, params string[] prefixs)
    {
        var prefixList = new string[] { "Dry." };
        if (prefixs is not null)
        {
            prefixList = prefixList.Union(prefixs).ToArray();
        }
        var implTypes = AssemblyHelper.GetAll(prefixList)
            .SelectMany(x => x.DefinedTypes)
            .Select(x => x.AsType())
            .Where(x => x.IsClass && !x.IsAbstract)
            .Where(x => x.GetInterfaces().Any(y => y == typeof(IDependency)))
            .ToArray();
        foreach (var implType in implTypes)
        {
            var genericTypes = implType.GetInterfaces().Where(x => x.IsGenericType).ToArray();

            var serviceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(IDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var serviceType in serviceTypes)
            {
                if (serviceType.IsAssignableFrom(implType))
                {
                    services.AddScoped(serviceType, implType);
                }
            }

            var transientServiceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(ITransientDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var transientServiceType in transientServiceTypes)
            {
                if (transientServiceType.IsAssignableFrom(implType))
                {
                    services.AddTransient(transientServiceType, implType);
                }
            }

            var singletonServiceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(ISingletonDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var singletonServiceType in singletonServiceTypes)
            {
                if (singletonServiceType.IsAssignableFrom(implType))
                {
                    services.AddSingleton(singletonServiceType, implType);
                }
            }
        }
        return services;
    }
}