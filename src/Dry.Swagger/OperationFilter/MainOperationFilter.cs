namespace Dry.Swagger.OperationFilter;

/// <summary>
/// 操作过滤器
/// </summary>
public sealed class MainOperationFilter : IOperationFilter
{
    /// <summary>
    /// 服务供应器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public MainOperationFilter(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
        => _serviceProvider.ServicesActionAsync<ICustomOperationFilter>(service =>
        {
            service.Apply(operation, context);
            return Task.CompletedTask;
        }).GetAwaiter().GetResult();
}