global using Dry.Core.Model;
global using Dry.Core.Utilities;
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
    /// <param name="prefixs">程序集命名前缀</param>
    /// <returns></returns>
    public static IServiceCollection AddDependency(this IServiceCollection services, params string[]? prefixs)
        => services.AddDependency(false, prefixs);

    /// <summary>
    /// 注入实现IDependency接口的类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="onlyLeaf">只注入叶子派生类</param>
    /// <param name="prefixs">程序集命名前缀</param>
    /// <returns></returns>
    public static IServiceCollection AddDependency(this IServiceCollection services, bool onlyLeaf, params string[]? prefixs)
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

        var serviceDescriptors = new List<ServiceDescriptor>();

        var getServiceDescriptor = ServiceDescriptor (Type serviceType, Type implType, ServiceLifetime lifetime) =>
        {
            var describeServiceType = serviceType;
            var describeImplType = implType;
            if (serviceType.IsGenericType && implType.IsGenericType)
            {
                var firstArgumentType = serviceType.GenericTypeArguments.FirstOrDefault();
                if (firstArgumentType is not null and { IsGenericTypeParameter: true })
                {
                    describeServiceType = serviceType.GetGenericTypeDefinition();
                    describeImplType = implType.GetGenericTypeDefinition();
                }
            }

#if NET8_0_OR_GREATER

            var serviceKey = implType.GetProperty(nameof(IDependency.ServiceKey))?.GetValue(null);
            if (serviceKey is not null)
            {
                return ServiceDescriptor.DescribeKeyed(describeServiceType, serviceKey, describeImplType, lifetime);
            }

#endif

            return ServiceDescriptor.Describe(describeServiceType, describeImplType, lifetime);
        };
        foreach (var implType in implTypes)
        {
            var genericTypes = implType.GetInterfaces().Where(x => x.IsGenericType).ToArray();

            var singletonServiceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(ISingletonDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var singletonServiceType in singletonServiceTypes)
            {
                if (singletonServiceType.IsAssignableFrom(implType))
                {
                    serviceDescriptors.Add(getServiceDescriptor(singletonServiceType, implType, ServiceLifetime.Singleton));
                }
            }

            var scopedServiceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(IDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var scopedServiceType in scopedServiceTypes)
            {
                if (scopedServiceType.IsAssignableFrom(implType))
                {
                    serviceDescriptors.Add(getServiceDescriptor(scopedServiceType, implType, ServiceLifetime.Scoped));
                }
            }

            var transientServiceTypes = genericTypes.Where(x => x.GetGenericTypeDefinition() == typeof(ITransientDependency<>)).Select(x => x.GenericTypeArguments.ElementAt(0)).ToArray();
            foreach (var transientServiceType in transientServiceTypes)
            {
                if (transientServiceType.IsAssignableFrom(implType))
                {
                    serviceDescriptors.Add(getServiceDescriptor(transientServiceType, implType, ServiceLifetime.Transient));
                }
            }
        }

        var getImplementationType = Type? (ServiceDescriptor serviceDescriptor) =>
        {

#if NET8_0_OR_GREATER

            if (serviceDescriptor.IsKeyedService)
            {
                return serviceDescriptor.KeyedImplementationType;
            }

#endif

            return serviceDescriptor.ImplementationType;
        };

        var serviceDescriptorGroups = serviceDescriptors.GroupBy(x => new { x.Lifetime, x.ServiceType }).ToArray();
        foreach (var serviceDescriptorGroup in serviceDescriptorGroups)
        {
            var filteredServiceDescriptors = onlyLeaf ? serviceDescriptorGroup.Where(x => !serviceDescriptorGroup.Any(y => getImplementationType(y) != getImplementationType(x) && getImplementationType(x)!.IsAssignableFrom(getImplementationType(y)))).ToArray() : serviceDescriptorGroup.ToArray();
            foreach (var filteredServiceDescriptor in filteredServiceDescriptors)
            {
                services.Add(filteredServiceDescriptor);
            }
        }

        return services;
    }
}