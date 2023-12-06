namespace Dry.Core.Utilities;

/// <summary>
/// 异常扩展
/// </summary>
public static class ExceptionExtension
{
    /// <summary>
    /// 转字符串消息
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string? ToMessage(this Exception? e)
    {
        if (e is null)
        {
            return null;
        }
        var msg = new
        {
            e.Message,
            e.Source,
            e.StackTrace,
            TargetSiteName = e.TargetSite?.Name,
            InnerException = ToMessage(e.InnerException)
        };
        return DryJsonSerializer.Serialize(msg);
    }
}