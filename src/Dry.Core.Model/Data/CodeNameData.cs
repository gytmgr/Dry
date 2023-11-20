#nullable disable

namespace Dry.Core.Model;

/// <summary>
/// 编码名称数据
/// </summary>
/// <typeparam name="TData"></typeparam>
public class CodeNameData<TData> : CodeData<TData>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}