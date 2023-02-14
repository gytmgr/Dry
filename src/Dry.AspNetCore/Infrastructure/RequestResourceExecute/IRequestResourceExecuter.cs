namespace Dry.AspNetCore.Infrastructure.RequestResourceExecute;

/// <summary>
/// 请求资源执行器接口
/// </summary>
public interface IRequestResourceExecuter : IHasOrder
{
    /// <summary>
    /// 进入执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutingAsync(ResourceExecutingContext context);

    /// <summary>
    /// 结束执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutedAsync(ResourceExecutedContext context);
}