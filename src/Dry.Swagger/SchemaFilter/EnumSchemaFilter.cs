using Dry.Core.Utilities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dry.Swagger.SchemaFilter
{
    /// <summary>
    /// 枚举过滤器
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 过滤器方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum)
            {
                return;
            }
            model.Description = $"{model.Description}（{EnumHelper.GetDescription(context.Type)}）";
        }
    }
}