namespace Dry.Application.Contracts.Dtos;

/// <summary>
/// 排序
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public class SortDto<TEnum> where TEnum : Enum
{
    /// <summary>
    /// 顺序，true为正序，false为倒序
    /// </summary>
    public bool Order { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    public TEnum Field { get; set; }
}