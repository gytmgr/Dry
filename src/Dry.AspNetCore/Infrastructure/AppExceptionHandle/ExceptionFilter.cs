namespace Dry.AspNetCore.Infrastructure.AppExceptionHandle;

/// <summary>
/// 异常过滤器
/// </summary>
public class ExceptionFilter : IAsyncExceptionFilter
{
    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual async Task OnExceptionAsync(ExceptionContext context)
    {
        try
        {
            await context.HttpContext.RequestServices.ServicesActionAsync<IAppExceptionHandler>(async handler => await handler.HandleAsync(context));
        }
        catch (Exception ex)
        {
            context.HttpContext.RequestServices.GetService<ILogger<IAppExceptionHandler>>()!.LogError(ex, "异常处理出错");
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
            };
        }
        context.ExceptionHandled = true;
    }
}