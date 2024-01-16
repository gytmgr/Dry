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
        try
        {
            await context.HttpContext.RequestServices.ServicesActionAsync<IRequestResultExecuter>(async executer => await executer.ExecutingAsync(context));
            var executedContext = await next();
            await context.HttpContext.RequestServices.ServicesActionAsync<IRequestResultExecuter>(async executer => await executer.ExecutedAsync(executedContext), false);
        }
        catch (BizException ex)
        {
            if (context.Result is null)
            {
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
        }
        catch (Exception ex)
        {
            context.HttpContext.RequestServices.GetService<ILogger<IRequestResultExecuter>>()!.LogError(ex, "结果过滤器出错");
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
            };
        }
    }
}