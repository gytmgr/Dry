namespace Dry.Swagger.SchemaFilter;

/// <summary>
/// 枚举过滤器
/// </summary>
public class EnumSchemaFilter : ICustomSchemaFilter
{
    /// <summary>
    /// 过滤器方法
    /// </summary>
    /// <param name="schema"></param>
    /// <param name="context"></param>
    public virtual void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum)
        {
            return;
        }
        schema.Description = $"{schema.Description}（{EnumHelper.GetDescription(context.Type)}）";
    }
}