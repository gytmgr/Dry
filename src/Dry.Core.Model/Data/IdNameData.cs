#nullable disable

namespace Dry.Core.Model;

/// <summary>
/// 标识名称数据
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TData"></typeparam>
public class IdNameData<TId, TData> : IdName<TId>
{
    /// <summary>
    /// 数据
    /// </summary>
    public TData Data { get; set; }
}