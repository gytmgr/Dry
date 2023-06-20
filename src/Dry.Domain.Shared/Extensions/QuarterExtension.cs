namespace Dry.Domain.Shared.Extensions;

/// <summary>
/// 季度扩展
/// </summary>
public static class QuarterExtension
{
    /// <summary>
    /// 获取指定季度的开始月份
    /// </summary>
    /// <param name="quarter"></param>
    /// <returns></returns>
    public static Month GetStartMonth(this Quarter quarter)
        => (Month)((byte)quarter * 3 - 2);
}