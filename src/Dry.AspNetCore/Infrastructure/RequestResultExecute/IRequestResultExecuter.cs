namespace Dry.AspNetCore.Infrastructure.RequestResultExecute;

/// <summary>
/// 请求结果执行器接口
/// </summary>
public interface IRequestResultExecuter : IHasOrder, ISingletonDependency<IRequestResultExecuter>
{
    /// <summary>
    /// 进入执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<bool> ExecutingAsync(ResultExecutingContext context);

    /// <summary>
    /// 结束执行
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task<bool> ExecutedAsync(ResultExecutedContext context);
}