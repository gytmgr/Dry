namespace Dry.AspNetCore.Controllers;

/// <summary>
/// 基础控制器
/// </summary>
public abstract class DryControllerBase : Controller
{
    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    protected virtual TService? Service<TService>()
        => HttpContext.RequestServices.GetService<TService>();

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    protected virtual TService RequiredService<TService>() where TService : notnull
        => HttpContext.RequestServices.GetRequiredService<TService>();

#if NET8_0_OR_GREATER

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    protected virtual TService? KeyedService<TService>(string serviceKey)
        => HttpContext.RequestServices.GetKeyedService<TService>(serviceKey);

    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceKey"></param>
    /// <returns></returns>
    protected virtual TService RequiredKeyedService<TService>(string serviceKey) where TService : notnull
        => HttpContext.RequestServices.GetRequiredKeyedService<TService>(serviceKey);

#endif

}