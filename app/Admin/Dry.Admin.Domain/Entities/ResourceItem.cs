namespace Dry.Admin.Domain.Entities;

/// <summary>
/// 资源项
/// </summary>
public class ResourceItem : IEntity, IAdminContext, IHasAddTime
{
    /// <summary>
    /// 资源id
    /// </summary>
    public Guid ResourceId { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public ResourceItemType Type { get; set; }

    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime AddTime { get; set; }

    #region 导航属性

    /// <summary>
    /// 系统资源
    /// </summary>
    public Resource Resource { get; set; }

    #endregion
}