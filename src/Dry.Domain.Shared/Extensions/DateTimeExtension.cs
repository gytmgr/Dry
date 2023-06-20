namespace Dry.Domain.Shared.Extensions;

/// <summary>
/// 时间扩展
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    /// 获取指定时间的季度
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static Quarter GetQuarter(this DateTime time)
        => ((Month)time.Month).GetQuarter();
}