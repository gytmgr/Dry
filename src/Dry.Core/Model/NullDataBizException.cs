namespace Dry.Core.Model
{
    /// <summary>
    /// 主数据不存在异常类
    /// </summary>
    public class NullDataBizException : BizException<string>
    {
        /// <summary>
        /// 异常消息
        /// </summary>
        public static string BizMessage = "数据不存在";

        /// <summary>
        /// 构造体
        /// </summary>
        public NullDataBizException() : this(BizMessage) { }

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="msg"></param>
        public NullDataBizException(string msg) : base(nameof(NullDataBizException), msg) { }
    }
}