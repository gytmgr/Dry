namespace Dry.Admin.Application.Services;

public class ResourceAppService(IServiceProvider serviceProvider) : ApplicationQueryServiceBase<Resource, ResourceDto, ResourceQueryDto, Guid>(serviceProvider), IResourceAppService, IDependency<IResourceAppService>
{
    protected override Expression<Func<Resource, bool>>[] GetPredicates(ResourceQueryDto queryDto)
    {
        var predicates = base.GetPredicates(queryDto).ToList();
        if (queryDto is not null)
        {
            if (!string.IsNullOrEmpty(queryDto.NameLike))
            {
                predicates.Add(x => x.Name.Contains(queryDto.NameLike));
            }
            if (queryDto.ParentId.HasValue)
            {
                predicates.Add(x => x.ParentId == queryDto.ParentId.Value);
            }
        }
        return [.. predicates];
    }
}