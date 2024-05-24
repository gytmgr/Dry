namespace Dry.Core.Utilities;

/// <summary>
/// 时间扩展
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    /// 获取中文星期
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string GetChineseWeekName(this DateTime dateTime)
        => dateTime.DayOfWeek switch
        {
            DayOfWeek.Sunday => "星期日",
            DayOfWeek.Monday => "星期一",
            DayOfWeek.Tuesday => "星期二",
            DayOfWeek.Wednesday => "星期三",
            DayOfWeek.Thursday => "星期四",
            DayOfWeek.Friday => "星期五",
            DayOfWeek.Saturday => "星期六",
            _ => "星期日"
        };

    /// <summary>
    /// 获取农历日期
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static string GetChineseLunisolarDate(this DateTime dateTime)
    {
        var cal = new ChineseLunisolarCalendar();
        var year = cal.GetYear(dateTime);
        var month = cal.GetMonth(dateTime);
        var day = cal.GetDayOfMonth(dateTime);
        var leapMonth = cal.GetLeapMonth(year);
        return $"农历{"甲乙丙丁戊己庚辛壬癸"[(year - 4) % 10]}{"子丑寅卯辰巳午未申酉戌亥"[(year - 4) % 12]}（{"鼠牛虎兔龙蛇马羊猴鸡狗猪"[(year - 4) % 12]}）年{(month == leapMonth ? "闰" : "")}{"无正二三四五六七八九十冬腊"[leapMonth > 0 && leapMonth <= month ? month - 1 : month]}月{"初十廿三"[day / 10]}{"日一二三四五六七八九"[day % 10]}";
    }

    /// <summary>
    /// 获取年龄
    /// </summary>
    /// <param name="fromDate">出生日期</param>
    /// <param name="toDate">目标日期（为空则取当前日期）</param>
    /// <returns></returns>
    public static int GetAge(this DateTime fromDate, DateTime? toDate = default)
    {
        toDate ??= DateTime.Today;
        var age = toDate.Value.Year - fromDate.Year;
        if (toDate.Value.DayOfYear < fromDate.DayOfYear)
        {
            age--;
        }
        return age;
    }

    /// <summary>
    /// 转Utc秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static double ToUtcSeconds(this DateTime dateTime)
        => (dateTime - DateTime.UnixEpoch).TotalSeconds;

    /// <summary>
    /// 转Utc毫秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static double ToUtcMilliseconds(this DateTime dateTime)
        => (dateTime - DateTime.UnixEpoch).TotalMilliseconds;

#if NET8_0_OR_GREATER

    /// <summary>
    /// 转Utc微秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static double ToUtcMicroseconds(this DateTime dateTime)
        => (dateTime - DateTime.UnixEpoch).TotalMicroseconds;

#endif
}