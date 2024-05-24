namespace Dry.Core.Utilities;

/// <summary>
/// 浮点数扩展
/// </summary>
public static class DoubleExtension
{
    /// <summary>
    /// Utc秒转DateTime
    /// </summary>
    /// <param name="utcSeconds"></param>
    /// <returns></returns>
    public static DateTime UtcSecondsToDateTime(this double utcSeconds)
        => new DateTime(1970, 1, 1).AddSeconds(utcSeconds);

    /// <summary>
    /// Utc毫秒转DateTime
    /// </summary>
    /// <param name="utcMilliseconds"></param>
    /// <returns></returns>
    public static DateTime UtcMillisecondsToDateTime(this double utcMilliseconds)
        => new DateTime(1970, 1, 1).AddMilliseconds(utcMilliseconds);

#if NET8_0_OR_GREATER

    /// <summary>
    /// Utc微妙转DateTime
    /// </summary>
    /// <param name="utcMicroseconds"></param>
    /// <returns></returns>
    public static DateTime UtcMicrosecondsToDateTime(this double utcMicroseconds)
        => new DateTime(1970, 1, 1).AddMicroseconds(utcMicroseconds);

#endif
}