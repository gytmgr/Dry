using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Dry.Mvc.Infrastructure
{
    /// <summary>
    /// 鉴权过滤器
    /// </summary>
    public class AuthorizationFilter : FilterBase<AuthorizationFilterContext>, IAuthorizationFilter
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="logger"></param>
        public AuthorizationFilter(ILogger<AuthorizationFilter> logger) : base(logger)
        {
            FilterActions.Add(Process);
        }

        /// <summary>
        /// 鉴权触发
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                {
                    return;
                }
                OnFilter(context);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "鉴权处理出错");
                context.Result = new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// 鉴权处理
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Process(AuthorizationFilterContext context)
        {
        }
    }
}