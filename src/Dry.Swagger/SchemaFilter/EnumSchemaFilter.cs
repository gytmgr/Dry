using Dry.Core.Utilities;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

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
            var descriptions = new List<string>();
            foreach (Enum value in Enum.GetValues(context.Type))
            {
                descriptions.Add($"{Convert.ToInt32(value)}：{value.GetDescription()}");
            }
            model.Description = $"{model.Description}（{string.Join("，", descriptions)}）";
        }
    }
}