using Dry.Mvc.Extensions;
using Dry.Mvc.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Dry.Mvc.Infrastructure
{
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
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}