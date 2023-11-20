namespace Dry.Core.Model;

/// <summary>
/// 业务异常类
/// </summary>
public class BizException : Exception
{
    /// <summary>
    /// 根据内部异常获取错误信息
    /// </summary>
    public static Func<Exception?, string>? GetMsgByInnerException { private get; set; }

    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    public override string Message => GetMsgByInnerException?.Invoke(InnerException) ?? base.Message;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="msg"></param>
    public BizException(string? msg) : base(msg) { }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="sysException"></param>
    public BizException(Exception? sysException) : base(sysException?.Message, sysException) { }

    /// <summary>
    /// 根据系统异常创建
    /// </summary>
    /// <param name="sysException"></param>
    /// <returns></returns>
    public static BizException CreateFromSysException(Exception sysException)
        => new(sysException);
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
    public BizException(TCode code, string? msg) : base(msg) => Code = code;
}