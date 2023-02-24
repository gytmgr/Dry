namespace Dry.AspNetCore.Infrastructure.RequestActionExecute;

/// <summary>
/// 操作过滤器
/// </summary>
public class ActionFilter : IAsyncActionFilter
{
    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public virtual async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await WebAppHelper.ServicesActionAsync<IRequestActionExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutingAsync(context));
        if (context.Result is null)
        {
            var executedContext = await next();
            await WebAppHelper.ServicesActionAsync<IRequestActionExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutedAsync(executedContext), false);
        }
    }
}