namespace Dry.Domain.Shared.Enums;

/// <summary>
/// 性别
/// </summary>
public enum Sex : byte
{
    /// <summary>
    /// 其它
    /// </summary>
    [Description("其它")]
    Other,

    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Man,

    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Woman
}