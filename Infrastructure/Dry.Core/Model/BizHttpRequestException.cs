using System.Net;

namespace Dry.Core.Model
{
    /// <summary>
    /// http请求异常类
    /// </summary>
    public class BizHttpRequestException : BizException<HttpStatusCode>
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public BizHttpRequestException(HttpStatusCode code, string msg = null) : base(code, msg) { }
    }
}