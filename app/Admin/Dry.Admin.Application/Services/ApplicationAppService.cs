namespace Dry.Admin.Application.Services;

public class ApplicationAppService : ApplicationQueryServiceBase<IAdminContext, App, ApplicationDto, ApplicationQueryDto, ApplicationCreateDto, ApplicationEditDto, string>, IApplicationAppService, IDependency<IApplicationAppService>
{
    public ApplicationAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override Expression<Func<App, bool>>[] GetPredicates(ApplicationQueryDto queryDto)
    {
        var predicates = base.GetPredicates(queryDto).ToList();
        if (queryDto is not null)
        {
            if (queryDto.TypeId.HasValue)
            {
                predicates.Add(x => x.Type == (ApplicationType)queryDto.TypeId.Value);
            }
            if (!string.IsNullOrEmpty(queryDto.NameLike))
            {
                predicates.Add(x => x.Name.Contains(queryDto.NameLike));
            }
            if (queryDto.Enable.HasValue)
            {
                predicates.Add(x => x.Enable == queryDto.Enable.Value);
            }
        }
        return predicates.ToArray();
    }

    protected override (bool isAsc, Expression<Func<App, dynamic>> keySelector)[] GetOrderBys(ApplicationQueryDto queryDto)
    {
        if (queryDto?.Sort is not null)
        {
            Expression<Func<App, dynamic>> keySelector = queryDto.Sort.Field switch
            {
                ApplicationQuerySortField.Type => x => x.Type,
                ApplicationQuerySortField.Name => x => x.Name,
                ApplicationQuerySortField.AddTime => x => x.AddTime,
                _ => x => x.AddTime
            };
            return new (bool isAsc, Expression<Func<App, dynamic>> keySelector)[] { queryDto.Sort.GetOrderByParam<App, ApplicationQuerySortField>().Value };
        }
        return base.GetOrderBys(queryDto);
    }

    public Task<KeyValuePair<int, string>[]> TypeArrayAsync()
    {
        var data = EnumHelper.GetEnumDic<ApplicationType>().ToArray();
        return Task.FromResult(data);
    }
}