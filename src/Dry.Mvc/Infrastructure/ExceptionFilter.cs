namespace Dry.Mvc.Infrastructure;

/// <summary>
/// 异常过滤器
/// </summary>
public class ExceptionFilter : FilterBase<ExceptionContext>, IExceptionFilter
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="logger"></param>
    public ExceptionFilter(ILogger<ExceptionFilter> logger) : base(logger)
    {
        FilterActions.Add(Process);
    }

    /// <summary>
    /// 异常触发
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnException(ExceptionContext context)
    {
        try
        {
            OnFilter(context);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "异常处理出错");
            context.Result = new StatusCodeResult(500);
        }
    }

    /// <summary>
    /// 异常处理
    /// </summary>
    /// <param name="context"></param>
    protected virtual void Process(ExceptionContext context)
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
                Logger.LogError(context.Exception, "未知异常");
                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
                };
                break;
        }
        context.ExceptionHandled = true;
    }
}