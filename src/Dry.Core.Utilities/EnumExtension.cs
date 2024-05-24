namespace Dry.Core.Utilities;

/// <summary>
/// 枚举扩展
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// 获取指定枚举项说明
    /// </summary>
    /// <param name="value"></param>
    /// <param name="nameInstead">是否用名称代替（没有说明时）</param>
    /// <returns></returns>
    public static string? GetDescription(this Enum value, bool nameInstead = true)
    {
        value.CheckParamNull(nameof(value));

        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name is null)
        {
            return null;
        }

        var field = type.GetField(name);
        if (field is null)
        {
            return null;
        }

        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        if (attribute is null && nameInstead)
        {
            return name;
        }
        return attribute?.Description;
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// 获取指定枚举项键值特性
    /// </summary>
    /// <typeparam name="TAttributeKey"></typeparam>
    /// <typeparam name="TAttributeValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static KeyValueAttribute<TAttributeKey, TAttributeValue>[]? GetAttribute<TAttributeKey, TAttributeValue>(this Enum value)
    {
        value.CheckParamNull(nameof(value));

        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name is null)
        {
            return default;
        }

        var field = type.GetField(name);
        if (field is null)
        {
            return default;
        }

        return Attribute.GetCustomAttributes(field, typeof(KeyValueAttribute<TAttributeKey, TAttributeValue>)) as KeyValueAttribute<TAttributeKey, TAttributeValue>[];
    }

    /// <summary>
    /// 获取指定枚举项键值
    /// </summary>
    /// <typeparam name="TAttributeKey"></typeparam>
    /// <typeparam name="TAttributeValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TAttributeValue? GetAttributeValue<TAttributeKey, TAttributeValue>(this Enum value, TAttributeKey key)
    {
        var attributeValue = value.GetAttribute<TAttributeKey, TAttributeValue>()?.FirstOrDefault(x => x.Key!.Equals(key));
        if (attributeValue is null)
        {
            return default;
        }

        return attributeValue.Value;
    }

    /// <summary>
    /// 获取指定枚举项键值
    /// </summary>
    /// <typeparam name="TAttributeValue"></typeparam>
    /// <param name="value"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static TAttributeValue? GetAttributeValue<TAttributeValue>(this Enum value, string key)
        => value.GetAttributeValue<string, TAttributeValue>(key);

#endif
}

/// <summary>
/// 枚举帮助类
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// 获取指定类型枚举列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<KeyValuePair<int, string?>> GetEnumDic<T>() where T : Enum
    {
        var type = typeof(T);
        foreach (Enum enumValue in Enum.GetValues(type))
        {
            yield return new KeyValuePair<int, string?>(Convert.ToInt32(enumValue), enumValue.GetDescription());
        }
    }

    /// <summary>
    /// 获取枚举所有项说明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="nameInstead">是否用名称代替（没有说明时）</param>
    /// <returns></returns>
    public static string? GetDescription<T>(bool nameInstead = true) where T : Enum
        => GetDescription(typeof(T), nameInstead);

    /// <summary>
    /// 获取枚举所有项说明
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <param name="nameInstead">是否用名称代替（没有说明时）</param>
    /// <returns></returns>
    public static string? GetDescription(Type enumType, bool nameInstead = true)
    {
        enumType.CheckParamNull(nameof(enumType));

        if (!enumType.IsEnum)
        {
            return null;
        }
        var descriptions = new List<string>();
        foreach (Enum value in Enum.GetValues(enumType))
        {
            descriptions.Add($"{Convert.ToInt32(value)}：{value.GetDescription(nameInstead)}");
        }
        return string.Join("，", descriptions);
    }
}