namespace Dry.AspNetCore.Infrastructure.RequestAuthorize;

/// <summary>
/// 请求鉴权器接口
/// </summary>
public interface IRequestAuthorizer : IHasOrder
{
    /// <summary>
    /// 鉴权
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task AuthorizeAsync(AuthorizationFilterContext context);
}