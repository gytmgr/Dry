#nullable disable

namespace Dry.Core.Model;

/// <summary>
/// 编码数据
/// </summary>
/// <typeparam name="TData"></typeparam>
public class CodeData<TData> : DryData<TData>
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}