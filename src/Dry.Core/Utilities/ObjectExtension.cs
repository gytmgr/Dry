namespace Dry.Core.Utilities;

/// <summary>
/// 对象扩展
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// 对象转单元素数组，如空则返回空
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T[] ToNullArray<T>(this T obj)
    {
        if (obj is null)
        {
            return null;
        }
        return new[] { obj };
    }

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopy<T>(this T obj)
    {
        //如果是字符串或值类型则直接返回
        if (obj is string || obj.GetType().IsValueType)
        {
            return obj;
        }
        else if (obj.GetType().IsSerializable)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
        else
        {

            var retval = Activator.CreateInstance(obj.GetType());
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopy(field.GetValue(obj)));
                }
                catch { }
            }
            return (T)retval;
        }
    }

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopyByJson<T>(this T obj)
    {
        var options = new JsonSerializerOptions().DefaultConfig();
        var json = JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopyToByJson<T>(this object obj)
    {
        var options = new JsonSerializerOptions().DefaultConfig();
        var json = JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <summary>
    /// 转大写
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToStringUpper<T>(this T obj)
    => obj?.ToString().ToUpper();

    /// <summary>
    /// 转小写
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToStringLower<T>(this T obj)
    => obj?.ToString().ToLower();

    /// <summary>
    /// 对象转url参数
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ObjectToUriParam(this object obj)
    {
        return string.Join("&", GetObjectParam(obj));
        static List<string> GetObjectParam(object obj, string propertyName = null)
        {
            var propertis = obj.GetType().GetProperties();
            var result = new List<string>();
            foreach (var property in propertis)
            {
                var name = string.IsNullOrEmpty(propertyName) ? property.Name : $"{propertyName}.{property.Name}";
                var value = property.GetValue(obj, null);

                if (value == null || value.Equals(property.PropertyType.DefaultValue()))
                {
                    continue;
                }
                if (value is IEnumerable array && !(value is string))
                {
                    var i = 0;
                    foreach (var item in array)
                    {
                        if (value.Equals(property.PropertyType.DefaultValue()))
                        {
                            continue;
                        }
                        if (item is null)
                        {
                            continue;
                        }
                        if (item.GetType().IsValueType || item is string)
                        {
                            result.Add($"{name}[{i}]={HttpUtility.UrlEncode(item.ToString())}");
                        }
                        else
                        {
                            result.AddRange(GetObjectParam(item, $"{name}[{i}]"));
                        }
                        i++;
                    }
                }
                else
                {
                    if (value.GetType().IsValueType || value is string)
                    {
                        result.Add($"{name}={HttpUtility.UrlEncode(value.ToString())}");
                    }
                    else
                    {
                        result.AddRange(GetObjectParam(value, name));
                    }
                }
            }
            return result;
        }
    }

    /// <summary>
    /// 拷贝属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fromObj"></param>
    /// <param name="toObj"></param>
    /// <param name="ignorePropertyNames"></param>
    /// <returns></returns>
    public static T CopyProperty<T>(this T fromObj, T toObj, params string[] ignorePropertyNames)
    {
        if (fromObj is not null and not string && !fromObj.GetType().IsValueType)
        {
            toObj ??= (T)Activator.CreateInstance(fromObj.GetType());
            var properties = fromObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                try
                {
                    if (ignorePropertyNames?.Where(x => x?.Contains('.') is false).Contains(property.Name) is true)
                    {
                        continue;
                    }
                    var fromPropertyValue = property.GetValue(fromObj);
                    if (fromPropertyValue is not null and not string && !property.PropertyType.IsValueType)
                    {
                        var toPropertyValue = property.GetValue(toObj);
                        var toPropertyIsNull = toPropertyValue is null;
                        var ignorePropertyNameStarts = $"{property.Name}.";
                        toPropertyValue = CopyProperty(fromPropertyValue, toPropertyValue,
                            ignorePropertyNames?.Where(x => x is not null && x.StartsWith(ignorePropertyNameStarts)).Select(x => x.Substring(ignorePropertyNameStarts.Length)).ToArray());
                        if (toPropertyIsNull)
                        {
                            property.SetValue(toObj, toPropertyValue);
                        }
                    }
                    else
                    {
                        property.SetValue(toObj, fromPropertyValue);
                    }
                }
                catch { }
            }
        }
        else
        {
            toObj = fromObj;
        }
        return toObj;
    }
}