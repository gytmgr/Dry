using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Dry.Mvc.Infrastructure
{
    /// <summary>
    /// 操作过滤器
    /// </summary>
    public class ActionFilter : FilterBase<ActionExecutingContext>, IActionFilter
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="logger"></param>
        public ActionFilter(ILogger<ActionFilter> logger) : base(logger)
        {
            FilterActions.Add(Process);
        }

        /// <summary>
        /// 操作进入触发
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                OnFilter(context);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "操作进入处理出错");
                context.Result = new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 操作进入处理
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Process(ActionExecutingContext context)
        {
        }

        /// <summary>
        /// 操作结束触发
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}