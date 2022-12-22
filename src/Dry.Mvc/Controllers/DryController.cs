global using Dry.Core.Model;
global using Dry.Mvc.Resources;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

namespace Dry.Mvc.Controllers;

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