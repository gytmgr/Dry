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

    /// <summary>
    /// 多服务操作
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceAction">返回true时，中断跳出</param>
    /// <param name="ascOrder"></param>
    /// <returns>是否中断跳出</returns>
    public static async Task<bool> ServicesActionAsync<TService>(this IServiceProvider serviceProvider, Func<TService, Task<bool>> serviceAction, bool ascOrder = true)
    {
        var executers = serviceProvider.GetServices<TService>();
        if (typeof(IHasOrder).IsAssignableFrom(typeof(TService)))
        {
            executers = ascOrder ? executers.OrderBy(x => ((IHasOrder)x!).Order) : executers.OrderByDescending(x => ((IHasOrder)x!).Order);
        }
        foreach (var executer in executers)
        {
            if (await serviceAction(executer))
            {
                return true;
            }
        }
        return false;
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

    /// <summary>
    /// 多服务操作
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="serviceKey"></param>
    /// <param name="serviceAction"></param>
    /// <param name="ascOrder"></param>
    /// <returns></returns>
    public static async Task<bool> ServicesActionAsync<TService>(this IServiceProvider serviceProvider, object serviceKey, Func<TService, Task<bool>> serviceAction, bool ascOrder = true)
    {
        var executers = serviceProvider.GetKeyedServices<TService>(serviceKey);
        if (typeof(IHasOrder).IsAssignableFrom(typeof(TService)))
        {
            executers = ascOrder ? executers.OrderBy(x => ((IHasOrder)x!).Order) : executers.OrderByDescending(x => ((IHasOrder)x!).Order);
        }
        foreach (var executer in executers)
        {
            if (await serviceAction(executer))
            {
                return true;
            }
        }
        return false;
    }

#endif

    /// <summary>
    /// 设置租户id
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static void SetTenantId(this IServiceProvider serviceProvider, string? tenantId)
        => serviceProvider.GetRequiredService<ITenantProvider>().Id = tenantId.EmptyToNull();

    /// <summary>
    /// 获取租户id
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static string? GetTenantId(this IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredService<ITenantProvider>().Id;

    /// <summary>
    /// 新建域并传递租户id
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IServiceScope CreateScopeWithTenantId(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        scope.ServiceProvider.SetTenantId(serviceProvider.GetTenantId());
        return scope;
    }

    /// <summary>
    /// 新建域并传递租户id
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static IServiceScope CreateScopeWithTenantId(this IServiceProvider serviceProvider, string? tenantId)
    {
        var scope = serviceProvider.CreateScope();
        scope.ServiceProvider.SetTenantId(tenantId.EmptyToNull());
        return scope;
    }
}