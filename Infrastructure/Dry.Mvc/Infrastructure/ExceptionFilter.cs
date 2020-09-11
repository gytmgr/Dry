using Dry.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Dry.Mvc.Infrastructure
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ExceptionFilter : FilterBase<ExceptionContext>, IExceptionFilter
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger) : base(logger)
        {
            FilterActions.Add(Process);
        }

        /// <summary>
        /// 异常触发
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            try
            {
                OnFilter(context);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "异常处理出错");
                context.Result = new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Process(ExceptionContext context)
        {
            var result = Result<int>.Create(-1);
            switch (context.Exception)
            {
                case BizException bizException:
                    result.Message = bizException.Message;
                    break;
                default:
                    result.Message = "系统错误，请重新操作，若问题仍未解决请联系管理员。";
                    Logger.LogError(context.Exception, "未知异常");
                    break;
            }
            context.Result = new JsonResult(result)
            {
                ContentType = "application/json"
            };
            context.ExceptionHandled = true;
        }
    }
}