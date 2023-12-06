namespace Dry.Dependency;

/// <summary>
/// 服务扩展
/// </summary>
public static class ServiceProviderExtension
{
    /// <summary>
    /// 多服务操作
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceAction"></param>
    /// <param name="ascOrder"></param>
    /// <returns></returns>
    public static async Task ServicesActionAsync<TService>(this IServiceProvider serviceProvider, Func<TService, Task> serviceAction, bool ascOrder = true)
    {
        var executers = serviceProvider.GetServices<TService>();
        if (typeof(IHasOrder).IsAssignableFrom(typeof(TService)))
        {
            executers = ascOrder ? executers.OrderBy(x => ((IHasOrder)x!).Order) : executers.OrderByDescending(x => ((IHasOrder)x!).Order);
        }
        foreach (var executer in executers)
        {
            await serviceAction(executer);
        }
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// 多服务操作
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceKey"></param>
    /// <param name="serviceAction"></param>
    /// <param name="ascOrder"></param>
    /// <returns></returns>
    public static async Task ServicesActionAsync<TService>(this IServiceProvider serviceProvider, object serviceKey, Func<TService, Task> serviceAction, bool ascOrder = true)
    {
        var executers = serviceProvider.GetKeyedServices<TService>(serviceKey);
        if (typeof(IHasOrder).IsAssignableFrom(typeof(TService)))
        {
            executers = ascOrder ? executers.OrderBy(x => ((IHasOrder)x!).Order) : executers.OrderByDescending(x => ((IHasOrder)x!).Order);
        }
        foreach (var executer in executers)
        {
            await serviceAction(executer);
        }
    }

#endif

}