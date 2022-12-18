namespace Dry.Admin.Domain.Shared.Enums;

/// <summary>
/// 资源项类型
/// </summary>
public enum ResourceItemType : byte
{
    /// <summary>
    /// 页面操作
    /// </summary>
    [Description("页面操作")]
    PageAction = 1,

    /// <summary>
    /// http方法
    /// </summary>
    [Description("http方法")]
    HttpMethod
}