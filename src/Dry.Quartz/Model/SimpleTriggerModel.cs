namespace Dry.Quartz.Model;

/// <summary>
/// 简单触发器
/// </summary>
public class SimpleTriggerModel : TriggerModel
{
    /// <summary>
    /// 执行间隔
    /// </summary>
    public TimeSpan Interval { get; set; }

    /// <summary>
    /// 重复执行次数（-1：一直重复）
    /// </summary>
    public int RepeatCount { get; set; }

    /// <summary>
    /// 检查
    /// </summary>
    /// <returns></returns>
    public override string Check()
    {
        if (Interval == default && RepeatCount != 0)
        {
            return "执行多次时，必须有间隔时间";
        }
        return base.Check();
    }
}