namespace Dry.Core.Model;

#if NET8_0_OR_GREATER

/// <summary>
/// 键值特性
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
/// <param name="key"></param>
[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class KeyValueAttribute<TKey, TValue>(TKey key) : Attribute
{
    /// <summary>
    /// 键
    /// </summary>
    public TKey Key { get; } = key;

    /// <summary>
    /// 值
    /// </summary>
    public TValue? Value { get; set; }
}

/// <summary>
/// 键值特性
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <param name="key"></param>
public class KeyValueAttribute<TValue>(string key) : KeyValueAttribute<string, TValue>(key)
{
}

#endif