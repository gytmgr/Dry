namespace Dry.Core.Model;

/// <summary>
/// 标识名称
/// </summary>
/// <typeparam name="TId"></typeparam>
public class IdName<TId> : IHasId<TId>
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public TId Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}