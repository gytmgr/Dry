namespace Dry.AspNetCore.Infrastructure.AppExceptionHandle;

/// <summary>
/// 异常处理器
/// </summary>
public class ExceptionHandler : IAppExceptionHandler
{
    public virtual int Order { get; set; } = int.MinValue;

    public virtual Task HandleAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BizException exception:
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    Content = exception.Message
                };
                break;
            default:
                context.HttpContext.RequestServices.GetService<ILogger<ExceptionHandler>>()!.LogError(context.Exception, "未知异常");
                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
                };
                break;
        }
        return Task.CompletedTask;
    }
}