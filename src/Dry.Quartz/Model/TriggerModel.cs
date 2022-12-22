namespace Dry.Quartz.Model;

/// <summary>
/// 触发器模型
/// </summary>
public class TriggerModel
{
    /// <summary>
    /// 映射键
    /// </summary>
    public const string MapKey = "TriggerParam";

    /// <summary>
    /// 主键
    /// </summary>
    public QuartzKey Key { get; set; }

    /// <summary>
    /// 作业主键
    /// </summary>
    public QuartzKey JobKey { get; internal set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 开始执行时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束执行时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 已执行次数
    /// </summary>
    public int ExecutedCount { get; internal set; }

    /// <summary>
    /// 状态
    /// </summary>
    public TriggerState State { get; internal set; }

    /// <summary>
    /// 检查
    /// </summary>
    /// <returns></returns>
    public virtual string Check() => null;
}