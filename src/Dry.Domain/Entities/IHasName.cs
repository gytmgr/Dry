namespace Dry.Domain.Entities;

/// <summary>
/// 有名称实体
/// </summary>
public interface IHasName
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}