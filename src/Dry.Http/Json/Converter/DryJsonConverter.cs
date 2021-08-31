using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dry.Http.Json.Converter
{
    /// <summary>
    /// json转换基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DryJsonConverter<T> : JsonConverter<T>
    {
        /// <summary>
        /// 写
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                switch (value)
                {
                    case ulong or uint or ushort or byte:
                        writer.WriteNumberValue(Convert.ToUInt64(value));
                        break;
                    case long or int or short or sbyte:
                        writer.WriteNumberValue(Convert.ToInt64(value));
                        break;
                    case float or double or decimal:
                        writer.WriteNumberValue(Convert.ToDecimal(value));
                        break;
                    case bool bVaule:
                        writer.WriteBooleanValue(bVaule);
                        break;
                    case Enum:
                        var underlyingType = Enum.GetUnderlyingType(typeof(T));
                        if (underlyingType == typeof(ulong) || underlyingType == typeof(uint) || underlyingType == typeof(ushort) || underlyingType == typeof(byte))
                        {
                            writer.WriteNumberValue(Convert.ToUInt64(value));
                        }
                        else
                        {
                            writer.WriteNumberValue(Convert.ToInt64(value));
                        }
                        break;
                    default:
                        writer.WriteStringValue(value.ToString());
                        break;
                }
            }
        }
    }
}