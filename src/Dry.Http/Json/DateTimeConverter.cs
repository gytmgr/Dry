using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dry.Http.Json
{
    /// <summary>
    /// 时间格式转换
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime);
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    /// <summary>
    /// 可为空时间格式转换
    /// </summary>
    public class DateTimeNullableConvert : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime?);
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}