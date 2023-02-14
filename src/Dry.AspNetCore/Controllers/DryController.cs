namespace Dry.AspNetCore.Controllers;

/// <summary>
/// 基础控制器
/// </summary>
public abstract class DryController : Controller
{
    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    protected virtual TService Service<TService>()
        => HttpContext.RequestServices.GetService<TService>();
}