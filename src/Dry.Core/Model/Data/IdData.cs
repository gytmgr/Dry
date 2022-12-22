namespace Dry.Core.Model;

/// <summary>
/// 标识数据
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TData"></typeparam>
public class IdData<TId, TData> : DryData<TData>, IHasId<TId>
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public TId Id { get; set; }
}