namespace Dry.AspNetCore.Infrastructure.RequestResultExecute;

/// <summary>
/// 结果过滤器
/// </summary>
public class ResultFilter : IAsyncResultFilter
{
    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public virtual async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        await WebAppHelper.ServicesActionAsync<IRequestResultExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutingAsync(context));
        var executedContext = await next();
        await WebAppHelper.ServicesActionAsync<IRequestResultExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutedAsync(executedContext), false);
    }
}