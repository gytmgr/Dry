namespace Dry.AspNetCore.Infrastructure;

/// <summary>
/// 无效状态返回工厂
/// </summary>
public static class InvalidModelStateResponseFactory
{
    /// <summary>
    /// 生成错误返回
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var errors = context.ModelState.Where(x => x.Value is not null).SelectMany(x => x.Value!.Errors).Select(x => x.ErrorMessage).ToArray();
        var response = new ErrorResource(messages: errors);
        return new BadRequestObjectResult(response);
    }
}