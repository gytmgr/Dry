using Dry.Json.Converter;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Dry.Json
{
    /// <summary>
    /// json序列化参数扩展
    /// </summary>
    public static class JsonSerializerOptionsExtension
    {
        /// <summary>
        /// 配置默认参数
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static JsonSerializerOptions DefaultConfig(this JsonSerializerOptions options)
        {
            //日期格式化
            options.Converters.Add(new StructJsonConverter<DateTime>());
            options.Converters.Add(new StructNullableJsonConverter<DateTime>());
            //时间格式化
            options.Converters.Add(new StructJsonConverter<TimeSpan>());
            options.Converters.Add(new StructNullableJsonConverter<TimeSpan>());
            //设置支持中文的unicode编码kds
            options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            //采用原始属性名称
            options.PropertyNamingPolicy = null;
            //启用缩进设置
            options.WriteIndented = true;
            return options;
        }
    }
}