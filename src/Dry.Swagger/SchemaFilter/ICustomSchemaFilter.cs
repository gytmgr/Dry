namespace Dry.Swagger.SchemaFilter;

/// <summary>
/// 自定义架构过滤器接口
/// </summary>
public interface ICustomSchemaFilter : ISchemaFilter, ISingletonDependency<ICustomSchemaFilter>
{
}