namespace Dry.Core.Utilities;

/// <summary>
/// 通用json转换
/// </summary>
/// <typeparam name="T"></typeparam>
public class CommonJsonConverter<T> : DryJsonConverter<T>
{
    /// <summary>
    /// 读
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readStr = reader.TokenType switch
        {
            JsonTokenType.Number => reader.GetDecimal().ToString(),
            JsonTokenType.True or JsonTokenType.False => reader.GetBoolean().ToString(),
            _ => reader.GetString()
        };
        if (readStr.TryParse(typeToConvert, out object? value))
        {
            return (T)value!;
        }
        else
        {
            return default(T);
        }
    }
}