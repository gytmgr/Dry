namespace Dry.Mvc.Resources;

/// <summary>
/// 错误资源
/// </summary>
public class ErrorResource
{
    /// <summary>
    /// 成功
    /// </summary>
    public string ErrorCode { get; private set; }

    /// <summary>
    /// 失败信息
    /// </summary>
    public string[] Messages { get; private set; }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="errorCode"></param>
    public ErrorResource(string errorCode)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="messages"></param>
    public ErrorResource(params string[] messages)
    {
        Messages = messages ?? new string[0];
    }

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="errorCode"></param>
    /// <param name="messages"></param>
    public ErrorResource(string errorCode, params string[] messages)
    {
        ErrorCode = errorCode;
        Messages = messages ?? new string[0];
    }
}