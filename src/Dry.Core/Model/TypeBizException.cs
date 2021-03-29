namespace Dry.Core.Model
{
    /// <summary>
    /// 类型异常类
    /// </summary>
    public class TypeBizException : BizException<string>
    {
        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="msg"></param>
        public TypeBizException(string msg) : base(nameof(TypeBizException), msg)
        {

        }
    }
}