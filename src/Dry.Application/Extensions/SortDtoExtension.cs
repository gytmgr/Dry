namespace Dry.Application.Extensions;

/// <summary>
/// 排序dto扩展
/// </summary>
public static class SortDtoExtension
{
    /// <summary>
    /// 获取排序参数
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="sortDto"></param>
    /// <returns></returns>
    public static (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)? GetOrderByParam<TSource, TEnum>(this SortDto<TEnum> sortDto) where TEnum : struct, Enum
    {
        var keyName = Enum.GetName(sortDto.Field);
        if (keyName is not null)
        {
            var keySelector = LinqHelper.GetKeySelector<TSource, dynamic>(keyName);
            if (keySelector is not null)
            {
                return (sortDto.Order, keySelector);
            }
        }
        return null;
    }

    /// <summary>
    /// 获取排序参数
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="sortDtos"></param>
    /// <returns></returns>
    public static (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)[] GetOrderByParams<TSource, TEnum>(this IEnumerable<SortDto<TEnum>> sortDtos) where TEnum : struct, Enum
        => sortDtos.Select(x => x.GetOrderByParam<TSource, TEnum>()).Where(x => x.HasValue).Select(x => x!.Value).ToArray();
}