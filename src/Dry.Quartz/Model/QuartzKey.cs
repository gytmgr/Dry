namespace Dry.Quartz.Model;

/// <summary>
/// 主键
/// </summary>
public struct QuartzKey
{
    private string _name;

    /// <summary>
    /// 默认名称
    /// </summary>
    public const string DefaultName = "DEFAULT";

    /// <summary>
    /// 名称
    /// </summary>
    public string Name
    {
        get
        {
            return _name ?? DefaultName;
        }
        set
        {
            _name = value;
        }
    }

    /// <summary>
    /// 分组
    /// </summary>
    public string Group { get; set; }
}