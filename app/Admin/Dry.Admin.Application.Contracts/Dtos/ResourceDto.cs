namespace Dry.Admin.Application.Contracts.Dtos;

/// <summary>
/// 系统资源dto
/// </summary>
public class ResourceDto : IResultDto
{
    /// <summary>
    /// 系统id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 上级资源id
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime AddTime { get; set; }
}

/// <summary>
/// 系统资源查询dto
/// </summary>
public class ResourceQueryDto : QueryDto<Guid>
{
    /// <summary>
    /// 名称模糊查询
    /// </summary>
    public string NameLike { get; set; }

    /// <summary>
    /// 上级资源id
    /// </summary>
    public Guid? ParentId { get; set; }
}