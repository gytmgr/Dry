namespace Dry.Mvc.Infrastructure;

/// <summary>
/// 过滤器基类
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class FilterBase<TContext> where TContext : FilterContext
{
    /// <summary>
    /// 区域
    /// </summary>
    protected string Area { get; set; }

    /// <summary>
    /// 控制器
    /// </summary>
    protected string Controller { get; set; }

    /// <summary>
    /// 操作
    /// </summary>
    protected string Action { get; set; }

    /// <summary>
    /// 日志
    /// </summary>
    protected ILogger Logger { get; private set; }

    /// <summary>
    /// 过滤操作
    /// </summary>
    protected List<Action<TContext>> FilterActions { get; } = new List<Action<TContext>>();

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="logger"></param>
    public FilterBase(ILogger logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// 过滤入口
    /// </summary>
    /// <param name="context"></param>
    protected virtual void OnFilter(TContext context)
    {
        Area = context.RouteData.Values["area"]?.ToString();
        Controller = context.RouteData.Values["controller"].ToString();
        Action = context.RouteData.Values["action"].ToString();
        foreach (var filterAction in FilterActions)
        {
            filterAction(context);
        }
    }
}