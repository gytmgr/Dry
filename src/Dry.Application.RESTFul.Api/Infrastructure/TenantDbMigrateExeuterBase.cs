namespace Dry.Application.RESTFul.Api.Infrastructure;

public abstract class TenantDbMigrateExeuterBase<TBoundedContext> : IRequestResourceExecuter where TBoundedContext : IBoundedContext
{
    protected HashSet<string?> _migratedTenantIds = new HashSet<string?>();

    public virtual int Order { get; set; } = 0;

    public virtual Task ExecutingAsync(ResourceExecutingContext context)
    {
        var tenant = context.HttpContext.RequestServices.GetRequiredService<ITenantProvider>();
        if (!_migratedTenantIds.Contains(tenant.Id))
        {
            lock (_migratedTenantIds)
            {
                if (!_migratedTenantIds.Contains(tenant.Id))
                {
                    context.HttpContext.RequestServices.GetRequiredService<IDomainApplicationService<TBoundedContext>>().DbMigrateAsync().GetAwaiter().GetResult();
                    _migratedTenantIds.Add(tenant.Id);
                }
            }
        }
        return Task.CompletedTask;
    }

    public virtual Task ExecutedAsync(ResourceExecutedContext context)
        => Task.CompletedTask;
}