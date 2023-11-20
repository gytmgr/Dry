namespace Dry.Quartz.Model;

/// <summary>
/// 作业模型
/// </summary>
public class JobModel
{
    /// <summary>
    /// 映射键
    /// </summary>
    public const string MapKey = "JobParam";

    /// <summary>
    /// 主键
    /// </summary>
    public QuartzKey Key { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 已执行次数
    /// </summary>
    public int ExecutedCount { get; internal set; }
}