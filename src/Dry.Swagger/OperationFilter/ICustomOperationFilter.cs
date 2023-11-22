namespace Dry.Swagger.OperationFilter;

/// <summary>
/// 自定义操作过滤器接口
/// </summary>
public interface ICustomOperationFilter : IOperationFilter, ISingletonDependency<ICustomOperationFilter>
{
}