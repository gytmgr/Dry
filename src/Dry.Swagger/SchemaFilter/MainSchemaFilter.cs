namespace Dry.Swagger.SchemaFilter;

/// <summary>
/// 架构过滤器
/// </summary>
public sealed class MainSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// 服务供应器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public MainSchemaFilter(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        => _serviceProvider.ServicesActionAsync<ICustomSchemaFilter>(service =>
        {
            service.Apply(schema, context);
            return Task.CompletedTask;
        }).GetAwaiter().GetResult();
}