namespace Dry.AspNetCore.Infrastructure.AppExceptionHandle;

/// <summary>
/// 应用异常处理器接口
/// </summary>
public interface IAppExceptionHandler : IHasOrder
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task HandleAsync(ExceptionContext context);
}