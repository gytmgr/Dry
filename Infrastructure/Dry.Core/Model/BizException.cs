using System;

namespace Dry.Core.Model
{
    /// <summary>
    /// 业务异常类
    /// </summary>
    public class BizException : ApplicationException
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="msg"></param>
        public BizException(string msg) : base(msg) { }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public BizException(string code, string msg) : base(msg) => Code = code;
    }
}