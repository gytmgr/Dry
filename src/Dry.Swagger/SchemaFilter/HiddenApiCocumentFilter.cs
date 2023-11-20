namespace Dry.Swagger.SchemaFilter;

/// <summary>
/// 接口隐藏特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HiddenApiAttribute : Attribute { }

/// <summary>
/// 隐藏接口过滤器
/// </summary>
public class HiddenApiCocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            if (apiDescription.TryGetMethodInfo(out MethodInfo method))
            {
                if (method.ReflectedType?.CustomAttributes.Any(x => x.AttributeType == typeof(HiddenApiAttribute)) is true || method.CustomAttributes.Any(x => x.AttributeType == typeof(HiddenApiAttribute)))
                {
                    var key = "/" + apiDescription.RelativePath;
                    if (key.Contains('?'))
                    {
                        var index = key.IndexOf("?", StringComparison.Ordinal);
                        key = key[..index];
                    }
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }
    }
}