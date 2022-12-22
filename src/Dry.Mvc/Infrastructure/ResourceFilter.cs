namespace Dry.Mvc.Infrastructure;

/// <summary>
/// 资源过滤器
/// </summary>
public class ResourceFilter : FilterBase<ResourceExecutingContext>, IResourceFilter
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="logger"></param>
    public ResourceFilter(ILogger<ResourceFilter> logger) : base(logger)
    {
        FilterActions.Add(Process);
    }

    /// <summary>
    /// 资源请求进入触发
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnResourceExecuting(ResourceExecutingContext context)
    {
        try
        {
            context.HttpContext.Request.EnableBuffering();
            OnFilter(context);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "资源请求进入处理出错");
            context.Result = new StatusCodeResult(500);
        }
    }

    /// <summary>
    /// 资源请求进入处理
    /// </summary>
    /// <param name="context"></param>
    protected virtual void Process(ResourceExecutingContext context)
    {
    }

    /// <summary>
    /// 资源请求结束触发
    /// </summary>
    /// <param name="context"></param>
    public virtual void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}