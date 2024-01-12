namespace Dry.Swagger.OperationFilter;

/// <summary>
/// 租户操作过滤器
/// </summary>
public class TenantOperationFilter : ICustomOperationFilter
{
    /// <summary>
    /// 默认租户id
    /// </summary>
    protected virtual string? DefaultTenantId { get; }

    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public virtual void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        var allowAnonymousAttr = bool () =>
        {
            if (context.ApiDescription.TryGetMethodInfo(out MethodInfo method))
            {
                if (method.ReflectedType?.CustomAttributes.Any(x => typeof(IAllowAnonymous).IsAssignableFrom(x.AttributeType)) is true || method.CustomAttributes.Any(x => typeof(IAllowAnonymous).IsAssignableFrom(x.AttributeType)))
                {
                    return true;
                }
            }
            return false;
        };
        if (!filterPipeline.Any(x => x.Filter is AuthorizeFilter) || filterPipeline.Any(x => x.Filter is IAllowAnonymousFilter) || allowAnonymousAttr())
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "TenantId",
                In = ParameterLocation.Header,
                Description = "租户id",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(DefaultTenantId)
                }
            });
        }
    }
}