namespace Dry.AspNetCore.Infrastructure.RequestResourceExecute;

/// <summary>
/// 资源过滤器
/// </summary>
public class ResourceFilter : IAsyncResourceFilter
{
    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public virtual async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        try
        {
            await WebAppHelper.ServicesActionAsync<IRequestResourceExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutingAsync(context));
            if (context.Result is null)
            {
                var executedContext = await next();
                await WebAppHelper.ServicesActionAsync<IRequestResourceExecuter>(context.HttpContext.RequestServices, async executer => await executer.ExecutedAsync(executedContext), false);
            }
        }
        catch (Exception ex)
        {
            context.HttpContext.RequestServices.GetService<ILogger<IRequestResourceExecuter>>().LogError(ex, "请求资源出错");
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
            };
        }
    }
}