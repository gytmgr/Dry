namespace Dry.Admin.Application.Mapping;

public class ResourceProfile : Profile
{
    public ResourceProfile()
    {
        CreateMap<Resource, ResourceDto>();
    }
}