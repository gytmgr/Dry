namespace Dry.Core.Utilities;

/// <summary>
/// json序列化
/// </summary>
public static class DryJsonSerializer
{
    /// <summary>
    /// 序列化
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static string Serialize<TValue>(TValue value, Action<JsonSerializerOptions>? setupAction = null)
    {
        var options = new JsonSerializerOptions().DefaultConfig();
        if (setupAction is not null)
        {
            setupAction(options);
        }
        return JsonSerializer.Serialize(value, options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="json"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static TValue? Deserialize<TValue>(string json, Action<JsonSerializerOptions>? setupAction = null)
    {
        json.CheckParamNull(nameof(json));

        var options = new JsonSerializerOptions().DefaultConfig();
        if (setupAction is not null)
        {
            setupAction(options);
        }
        return JsonSerializer.Deserialize<TValue>(json, options);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="json"></param>
    /// <param name="returnType"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static object? Deserialize(string json, Type returnType, Action<JsonSerializerOptions>? setupAction = null)
    {
        json.CheckParamNull(nameof(json));

        var options = new JsonSerializerOptions().DefaultConfig();
        if (setupAction is not null)
        {
            setupAction(options);
        }
        return JsonSerializer.Deserialize(json, returnType, options);
    }
}