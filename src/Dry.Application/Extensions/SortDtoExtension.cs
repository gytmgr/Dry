global using AutoMapper;
global using Dry.Application.Contracts.Dtos;
global using Dry.Application.Contracts.Services;
global using Dry.Core.Model;
global using Dry.Core.Utilities;
global using Dry.Domain;
global using Dry.Domain.Entities;
global using Dry.Domain.Extensions;
global using Dry.Domain.Repositories;
global using Microsoft.Extensions.DependencyInjection;
global using System.Diagnostics.CodeAnalysis;
global using System.Linq.Expressions;

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
    public static (bool isAsc, Expression<Func<TSource, dynamic>> keySelector) GetOrderByParam<TSource, TEnum>(this SortDto<TEnum> sortDto) where TEnum : struct, Enum
        => (sortDto.Order, LinqHelper.GetKeySelector<TSource, dynamic>(Enum.GetName(sortDto.Field)));

    /// <summary>
    /// 获取排序参数
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="sortDtos"></param>
    /// <returns></returns>
    public static (bool isAsc, Expression<Func<TSource, dynamic>> keySelector)[] GetOrderByParams<TSource, TEnum>(this IEnumerable<SortDto<TEnum>> sortDtos) where TEnum : struct, Enum
        => sortDtos.Select(x => x.GetOrderByParam<TSource, TEnum>()).ToArray();
}