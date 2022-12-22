namespace Dry.Core.Utilities;

/// <summary>
/// 字符串扩展
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 转换为可为空的bool
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool? ToBool(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (bool.TryParse(str, out bool result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的sbyte
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static sbyte? ToSByte(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (sbyte.TryParse(str, out sbyte result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的byte
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte? ToByte(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (byte.TryParse(str, out byte result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的short
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static short? ToShort(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (short.TryParse(str, out short result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的ushort
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static ushort? ToUShort(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (ushort.TryParse(str, out ushort result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int? ToInt(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的uint
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static uint? ToUInt(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (uint.TryParse(str, out uint result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的long
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static long? ToLong(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (long.TryParse(str, out long result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的ulong
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static ulong? ToULong(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (ulong.TryParse(str, out ulong result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float? ToFloat(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (float.TryParse(str, out float result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的double
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double? ToDouble(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (double.TryParse(str, out double result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的decimal
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static decimal? ToDecimal(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (decimal.TryParse(str, out decimal result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的char
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static char? ToChar(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (char.TryParse(str, out char result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的Enum
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    public static TEnum? ToEnum<TEnum>(this string str) where TEnum : struct
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (Enum.TryParse(str, out TEnum result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的guid
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Guid? ToGuid(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (Guid.TryParse(str, out Guid result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的DateTime
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime? ToDateTime(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (DateTime.TryParse(str, out DateTime result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 转换为可为空的TimeSpan
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static TimeSpan? ToTimeSpan(this string str)
    {
        if (!string.IsNullOrWhiteSpace(str))
        {
            if (TimeSpan.TryParse(str, out TimeSpan result))
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 将空字符处理为null
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string EmptyToNull(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return null;
        }
        return str.Trim();
    }

    /// <summary>
    /// 将null处理为Empty
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string NullToEmpty(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return string.Empty;
        }
        return str.Trim();
    }

    /// <summary>
    /// 字符串转结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <param name="outResult"></param>
    /// <returns></returns>
    public static bool TryParse<T>(this string str, out T outResult) where T : struct
    {
        outResult = default;
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }
        var type = typeof(T);
        if (type.IsEnum)
        {
            return Enum.TryParse(str, out outResult);
        }
        var tryParseMethod = type.GetMethod("TryParse"
            , BindingFlags.Public | BindingFlags.Static
            , Type.DefaultBinder
            , new Type[] { typeof(string), type.MakeByRefType() }
            , new ParameterModifier[] { new ParameterModifier(2) });
        if (tryParseMethod is not null)
        {
            var parameters = new object[] { str, Activator.CreateInstance(type) };
            var success = (bool)tryParseMethod.Invoke(null, parameters);
            if (success)
            {
                outResult = (T)parameters[1];
                return success;
            }
        }
        return false;
    }

    /// <summary>
    /// 字符串转值类型
    /// </summary>
    /// <param name="str"></param>
    /// <param name="outType"></param>
    /// <param name="outResult"></param>
    /// <returns></returns>
    public static bool TryParse(this string str, Type outType, out object outResult)
    {
        outResult = default;
        if (!outType.IsValueType)
        {
            return false;
        }
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }
        if (outType.IsEnum)
        {
            return Enum.TryParse(outType, str, out outResult);
        }
        var tryParseMethod = outType.GetMethod("TryParse"
            , BindingFlags.Public | BindingFlags.Static
            , Type.DefaultBinder
            , new Type[] { typeof(string), outType.MakeByRefType() }
            , new ParameterModifier[] { new ParameterModifier(2) });
        if (tryParseMethod is not null)
        {
            var parameters = new object[] { str, Activator.CreateInstance(outType) };
            var success = (bool)tryParseMethod.Invoke(null, parameters);
            if (success)
            {
                outResult = parameters[1];
                return success;
            }
        }
        return false;
    }
}