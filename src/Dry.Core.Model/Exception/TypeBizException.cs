namespace Dry.Core.Model;

/// <summary>
/// 类型异常类
/// </summary>
public class TypeBizException : BizException<string>
{
    /// <summary>
    /// 异常消息
    /// </summary>
    public static string BizMessage = "类型错误";

    /// <summary>
    /// 构造体
    /// </summary>
    public TypeBizException() : this(BizMessage) { }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="msg"></param>
    public TypeBizException(string msg) : base(nameof(TypeBizException), msg) { }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="code"></param>
    /// <param name="msg"></param>
    public TypeBizException(string code, string? msg) : base(code, msg) { }
}