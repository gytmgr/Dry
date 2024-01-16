namespace Dry.AspNetCore.Infrastructure.RequestResourceExecute;

public class RequestResourceExecuter : IRequestResourceExecuter
{
    public virtual int Order { get; set; } = int.MinValue;

    public virtual Task ExecutingAsync(ResourceExecutingContext context)
    {
        var tenantId = context.HttpContext.Request.Headers[ITenantProvider.IdKey].FirstOrDefault();
        context.HttpContext.RequestServices.SetTenantId(tenantId);
        return Task.CompletedTask;
    }

    public virtual Task ExecutedAsync(ResourceExecutedContext context)
        => Task.CompletedTask;
}