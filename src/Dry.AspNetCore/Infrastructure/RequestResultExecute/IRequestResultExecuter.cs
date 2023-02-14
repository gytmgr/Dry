namespace Dry.AspNetCore.Infrastructure.RequestResultExecute;

/// <summary>
/// 请求结果执行器接口
/// </summary>
public interface IRequestResultExecuter : IHasOrder
{
    /// <summary>
    /// 进入执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutingAsync(ResultExecutingContext context);

    /// <summary>
    /// 结束执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ExecutedAsync(ResultExecutedContext context);
}