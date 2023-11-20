namespace Dry.Core.Model;

/// <summary>
/// 有顺序属性
/// </summary>
public interface IHasOrder
{
    /// <summary>
    /// 顺序
    /// </summary>
    int Order { get; set; }
}