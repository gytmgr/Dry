using AutoMapper;
using Dry.Admin.Application.Contracts.Dtos;
using Dry.Admin.Domain.Shared.Enums;
using Dry.Application.Contracts.Dtos;
using Dry.Core.Utilities;
using App = Dry.Admin.Domain.Entities.Application;

namespace Dry.Admin.Application.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ValueDto<int>, ApplicationType>().ConvertUsing((x, y) => x == null ? y : (ApplicationType)x.Value);
            CreateMap<App, ApplicationDto>()
                .ForMember(x => x.TypeId, x => x.MapFrom(y => (int)y.Type))
                .ForMember(x => x.TypeName, x => x.MapFrom(y => y.Type.GetDescription(true)));
            CreateMap<ApplicationCreateDto, App>();
            CreateMap<ApplicationEditDto, App>();
        }
    }
}