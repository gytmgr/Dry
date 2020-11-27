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
            CreateMap<ValueDto<Guid>, Guid>().ConvertUsing(Convert);
            CreateMap<ValueDto<Guid?>, Guid?>().ConvertUsing(Convert);
            CreateMap<ValueDto<bool>, bool>().ConvertUsing(Convert);
            CreateMap<ValueDto<bool?>, bool?>().ConvertUsing(Convert);
        }

        private TValue Convert<TValue>(ValueDto<TValue> source, TValue destination)
            => source == null ? destination : source.Value;
    }
}