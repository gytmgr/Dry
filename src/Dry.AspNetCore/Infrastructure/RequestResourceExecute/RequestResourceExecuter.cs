namespace Dry.AspNetCore.Infrastructure.RequestResourceExecute;

public class RequestResourceExecuter : IRequestResourceExecuter
{
    protected const string _tenantIdKey = "TenantId";

    public virtual int Order { get; set; } = int.MinValue;

    public virtual Task ExecutingAsync(ResourceExecutingContext context)
    {
        var tenant = context.HttpContext.RequestServices.GetRequiredService<ITenantProvider>();
        tenant.Id = context.HttpContext.Request.Headers[_tenantIdKey].FirstOrDefault().EmptyToNull();
        return Task.CompletedTask;
    }

    public virtual Task ExecutedAsync(ResourceExecutedContext context)
        => Task.CompletedTask;
}