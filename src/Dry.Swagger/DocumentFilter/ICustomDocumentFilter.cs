namespace Dry.Swagger.DocumentFilter;

/// <summary>
/// 自定义文档过滤器接口
/// </summary>
public interface ICustomDocumentFilter : IDocumentFilter, ISingletonDependency<ICustomDocumentFilter>
{
}