using AutoMapper;
using Dry.Core.Model;
using System;

namespace Dry.AutoMapper.Mapping
{
    /// <summary>
    /// 值对象映射
    /// </summary>
    public class DryProfile : Profile
    {
        /// <summary>
        /// 构造体
        /// </summary>
        public DryProfile()
        {
            CreateMap<DryData<char>, char>().ConvertUsing(MappingFunction);
            CreateMap<DryData<string>, string>().ConvertUsing(MappingFunction);
            CreateMap<DryData<sbyte>, sbyte>().ConvertUsing(MappingFunction);
            CreateMap<DryData<sbyte?>, sbyte?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<byte>, byte>().ConvertUsing(MappingFunction);
            CreateMap<DryData<byte?>, byte?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<int>, int>().ConvertUsing(MappingFunction);
            CreateMap<DryData<int?>, int?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<uint>, uint>().ConvertUsing(MappingFunction);
            CreateMap<DryData<uint?>, uint?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<long>, long>().ConvertUsing(MappingFunction);
            CreateMap<DryData<long?>, long?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<ulong>, ulong>().ConvertUsing(MappingFunction);
            CreateMap<DryData<ulong?>, ulong?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<float>, float>().ConvertUsing(MappingFunction);
            CreateMap<DryData<float?>, float?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<double>, double>().ConvertUsing(MappingFunction);
            CreateMap<DryData<double?>, double?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<decimal>, decimal>().ConvertUsing(MappingFunction);
            CreateMap<DryData<decimal?>, decimal?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<Guid>, Guid>().ConvertUsing(MappingFunction);
            CreateMap<DryData<Guid?>, Guid?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<bool>, bool>().ConvertUsing(MappingFunction);
            CreateMap<DryData<bool?>, bool?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<DateTime>, DateTime>().ConvertUsing(MappingFunction);
            CreateMap<DryData<DateTime?>, DateTime?>().ConvertUsing(MappingFunction);
#if NET6_0
            CreateMap<DryData<DateOnly>, DateOnly>().ConvertUsing(MappingFunction);
            CreateMap<DryData<DateOnly?>, DateOnly?>().ConvertUsing(MappingFunction);
            CreateMap<DryData<TimeOnly>, TimeOnly>().ConvertUsing(MappingFunction);
            CreateMap<DryData<TimeOnly?>, TimeOnly?>().ConvertUsing(MappingFunction);
#endif
            CreateMap<DryData<TimeSpan>, TimeSpan>().ConvertUsing(MappingFunction);
            CreateMap<DryData<TimeSpan?>, TimeSpan?>().ConvertUsing(MappingFunction);

            CreateMap<char, DryData<char>>().ConvertUsing(MappingFunction);
            CreateMap<string, DryData<string>>().ConvertUsing(MappingFunction);
            CreateMap<sbyte, DryData<sbyte>>().ConvertUsing(MappingFunction);
            CreateMap<sbyte?, DryData<sbyte?>>().ConvertUsing(MappingFunction);
            CreateMap<byte, DryData<byte>>().ConvertUsing(MappingFunction);
            CreateMap<byte?, DryData<byte?>>().ConvertUsing(MappingFunction);
            CreateMap<int, DryData<int>>().ConvertUsing(MappingFunction);
            CreateMap<int?, DryData<int?>>().ConvertUsing(MappingFunction);
            CreateMap<uint, DryData<uint>>().ConvertUsing(MappingFunction);
            CreateMap<uint?, DryData<uint?>>().ConvertUsing(MappingFunction);
            CreateMap<long, DryData<long>>().ConvertUsing(MappingFunction);
            CreateMap<long?, DryData<long?>>().ConvertUsing(MappingFunction);
            CreateMap<ulong, DryData<ulong>>().ConvertUsing(MappingFunction);
            CreateMap<ulong?, DryData<ulong?>>().ConvertUsing(MappingFunction);
            CreateMap<float, DryData<float>>().ConvertUsing(MappingFunction);
            CreateMap<float?, DryData<float?>>().ConvertUsing(MappingFunction);
            CreateMap<double, DryData<double>>().ConvertUsing(MappingFunction);
            CreateMap<double?, DryData<double?>>().ConvertUsing(MappingFunction);
            CreateMap<decimal, DryData<decimal>>().ConvertUsing(MappingFunction);
            CreateMap<decimal?, DryData<decimal?>>().ConvertUsing(MappingFunction);
            CreateMap<Guid, DryData<Guid>>().ConvertUsing(MappingFunction);
            CreateMap<Guid?, DryData<Guid?>>().ConvertUsing(MappingFunction);
            CreateMap<bool, DryData<bool>>().ConvertUsing(MappingFunction);
            CreateMap<bool?, DryData<bool?>>().ConvertUsing(MappingFunction);
            CreateMap<DateTime, DryData<DateTime>>().ConvertUsing(MappingFunction);
            CreateMap<DateTime?, DryData<DateTime?>>().ConvertUsing(MappingFunction);
#if NET6_0
            CreateMap<DateOnly, DryData<DateOnly>>().ConvertUsing(MappingFunction);
            CreateMap<DateOnly?, DryData<DateOnly?>>().ConvertUsing(MappingFunction);
            CreateMap<TimeOnly, DryData<TimeOnly>>().ConvertUsing(MappingFunction);
            CreateMap<TimeOnly?, DryData<TimeOnly?>>().ConvertUsing(MappingFunction);
#endif
            CreateMap<TimeSpan, DryData<TimeSpan>>().ConvertUsing(MappingFunction);
            CreateMap<TimeSpan?, DryData<TimeSpan?>>().ConvertUsing(MappingFunction);
        }

        /// <summary>
        /// DryData转Data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TData MappingFunction<TData>(DryData<TData> source, TData destination)
            => source is null ? destination : source.Data;

        /// <summary>
        /// Data转DryData
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static DryData<TData> MappingFunction<TData>(TData source, DryData<TData> destination)
            => new() { Data = source };
    }
}