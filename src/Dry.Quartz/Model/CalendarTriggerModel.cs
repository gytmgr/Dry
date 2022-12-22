namespace Dry.Quartz.Model;

/// <summary>
/// 日间间隔触发器
/// </summary>
public class CalendarTriggerModel : TriggerModel
{
    /// <summary>
    /// 间隔单位
    /// </summary>
    public IntervalUnit Unit { get; set; }

    /// <summary>
    /// 间隔值
    /// </summary>
    public int Interval { get; set; } = 1;

    /// <summary>
    /// 检查
    /// </summary>
    /// <returns></returns>
    public override string Check()
    {
        if (Unit is IntervalUnit.Millisecond)
        {
            return "间隔单位不能是毫秒";
        }
        if (Interval < 1)
        {
            return "间隔值必须大于0";
        }
        return base.Check();
    }
}