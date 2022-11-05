using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Dry.Dependency
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注入实现IDependency接口的类型
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            var implTypes = AssemblyHelper.GetAll()
                .SelectMany(x => x.DefinedTypes)
                .Select(x => x.AsType())
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IDependency<>)))
                .ToArray();
            foreach (var implType in implTypes)
            {
                var serviceTypes = implType.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDependency<>))
                    .Select(x => x.GenericTypeArguments.ElementAt(0))
                    .ToArray();
                foreach (var serviceType in serviceTypes)
                {
                    if (serviceType.IsAssignableFrom(implType))
                    {
                        services.AddScoped(serviceType, implType);
                    }
                }
            }
            return services;
        }
    }
}