using AutoMapper;
using Dry.Application.Contracts.Dtos;
using System;

namespace Dry.Application.Mapping
{
    /// <summary>
    /// 值对象映射
    /// </summary>
    public class ValueProfile : Profile
    {
        /// <summary>
        /// 构造体
        /// </summary>
        public ValueProfile()
        {
            CreateMap<ValueDto<string>, string>().ConvertUsing(Convert);
            CreateMap<ValueDto<byte>, byte>().ConvertUsing(Convert);
            CreateMap<ValueDto<byte?>, byte?>().ConvertUsing(Convert);
            CreateMap<ValueDto<int>, int>().ConvertUsing(Convert);
            CreateMap<ValueDto<int?>, int?>().ConvertUsing(Convert);
            CreateMap<ValueDto<long>, long>().ConvertUsing(Convert);
            CreateMap<ValueDto<long?>, long?>().ConvertUsing(Convert);
            CreateMap<ValueDto<Guid>, Guid>().ConvertUsing(Convert);
            CreateMap<ValueDto<Guid?>, Guid?>().ConvertUsing(Convert);
            CreateMap<ValueDto<bool>, bool>().ConvertUsing(Convert);
            CreateMap<ValueDto<bool?>, bool?>().ConvertUsing(Convert);
            CreateMap<ValueDto<DateTime>, DateTime>().ConvertUsing(Convert);
            CreateMap<ValueDto<DateTime?>, DateTime?>().ConvertUsing(Convert);
            CreateMap<ValueDto<TimeSpan>, TimeSpan>().ConvertUsing(Convert);
            CreateMap<ValueDto<TimeSpan?>, TimeSpan?>().ConvertUsing(Convert);
        }

        /// <summary>
        /// ValueDto转Value
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public TValue Convert<TValue>(ValueDto<TValue> source, TValue destination)
            => source is null ? destination : source.Value;
    }
}