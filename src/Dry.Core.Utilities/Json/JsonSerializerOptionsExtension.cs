namespace Dry.Core.Utilities;

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
        options.CheckParamNull(nameof(options));

        options.Converters.Add(new StructJsonConverter<DateTime>());
        options.Converters.Add(new StructNullableJsonConverter<DateTime>());
        options.Converters.Add(new StructJsonConverter<TimeSpan>());
        options.Converters.Add(new StructNullableJsonConverter<TimeSpan>());
        options.Converters.Add(new StructJsonConverter<DateOnly>());
        options.Converters.Add(new StructNullableJsonConverter<DateOnly>());
        options.Converters.Add(new StructJsonConverter<TimeOnly>());
        options.Converters.Add(new StructNullableJsonConverter<TimeOnly>());
        //设置支持中文的unicode编码kds
        options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        //采用原始属性名称
        options.PropertyNamingPolicy = null;
        //启用缩进设置
        options.WriteIndented = true;
        return options;
    }
}