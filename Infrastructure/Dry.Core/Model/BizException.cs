using System;

namespace Dry.Core.Model
{
    /// <summary>
    /// 业务异常类
    /// </summary>
    public class BizException : Exception
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="msg"></param>
        public BizException(string msg) : base(msg) { }
    }

    /// <summary>
    /// 业务异常类
    /// </summary>
    /// <typeparam name="TCode"></typeparam>
    public class BizException<TCode> : BizException
    {
        /// <summary>
        /// 编码
        /// </summary>
        public TCode Code { get; }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public BizException(TCode code, string msg) : base(msg) => Code = code;
    }
}