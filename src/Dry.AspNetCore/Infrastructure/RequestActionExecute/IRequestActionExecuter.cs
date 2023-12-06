namespace Dry.AspNetCore.Infrastructure.RequestActionExecute;

/// <summary>
/// 请求操作执行器接口
/// </summary>
public interface IRequestActionExecuter : IHasOrder, ISingletonDependency<IRequestActionExecuter>
{
    /// <summary>
    /// 进入执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutingAsync(ActionExecutingContext context);

    /// <summary>
    /// 结束执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutedAsync(ActionExecutedContext context);
}