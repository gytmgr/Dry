namespace Dry.AspNetCore.Infrastructure.RequestAuthorize;

/// <summary>
/// 鉴权过滤器
/// </summary>
public class AuthorizationFilter : AuthorizeFilter
{
    /// <summary>
    /// 触发
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        await base.OnAuthorizationAsync(context);
        try
        {
            await WebAppHelper.ServicesActionAsync<IRequestAuthorizer>(context.HttpContext.RequestServices, async authorizer => await authorizer.AuthorizeAsync(context));
        }
        catch (Exception ex)
        {
            context.HttpContext.RequestServices.GetService<ILogger<IRequestAuthorizer>>().LogError(ex, "鉴权出错");
            context.Result = new ContentResult
            {
                StatusCode = 500,
                Content = "系统错误，请重新操作，若问题仍未解决请联系管理员。"
            };
        }
    }
}