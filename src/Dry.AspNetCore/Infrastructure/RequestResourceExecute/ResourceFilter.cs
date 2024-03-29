﻿namespace Dry.AspNetCore.Infrastructure.RequestResourceExecute;

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
            await context.HttpContext.RequestServices.ServicesActionAsync<IRequestResourceExecuter>(async executer =>
            {
                await executer.ExecutingAsync(context);
                return context.Result is not null;
            });
            if (context.Result is null)
            {
                var executedContext = await next();
                await context.HttpContext.RequestServices.ServicesActionAsync<IRequestResourceExecuter>(async executer =>
                {
                    await executer.ExecutedAsync(executedContext);
                    return context.Result is not null;
                }, false);
            }
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
            context.HttpContext.RequestServices.GetService<ILogger<IRequestResourceExecuter>>()!.LogError(ex, "资源过滤器出错");
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
            };
        }
    }
}