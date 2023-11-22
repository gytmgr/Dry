namespace Dry.Swagger.DocumentFilter;

/// <summary>
/// 文档过滤器
/// </summary>
public sealed class MainDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 服务供应器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public MainDocumentFilter(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        => _serviceProvider.ServicesActionAsync<ICustomDocumentFilter>(service =>
        {
            service.Apply(swaggerDoc, context);
            return Task.CompletedTask;
        }).GetAwaiter().GetResult();
}