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
    public static T[]? ToNullArray<T>(this T? obj)
    {
        if (obj is null)
        {
            return null;
        }
        return new[] { obj };
    }

    /// <summary>
    /// 检查参数是否为null（为null抛异常）
    /// </summary>
    /// <param name="param">参数</param>
    /// <param name="paramName">参数名</param>
    /// <param name="bizCheck">是否业务检查（是否抛业务异常）</param>
    public static void CheckParamNull(this object? param, string paramName, bool bizCheck = false)
    {
        if (param is null)
        {
            var exception = new ArgumentNullException(paramName);
            if (bizCheck)
            {
                throw BizException.CreateFromSysException(exception);
            }
            throw exception;
        }
    }

    /// <summary>
    /// 检查参数属性是否为null（为null抛系统异常）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="param">参数</param>
    /// <param name="paramName">参数名</param>
    /// <param name="propertyNames">属性名</param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void CheckParamPropertyNull<T>(this T param, string paramName, params string[]? propertyNames) where T : class
    {
        param.CheckParamNull(paramName);
        if (propertyNames?.Length > 0)
        {
            var properties = param.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var nullPropertyNames = properties.Where(x => propertyNames.Contains(x.Name) && x.GetValue(param) is null).Select(x => x.Name).ToArray();
            if (nullPropertyNames.Length > 0)
            {
                throw new ArgumentNullException($"参数【{paramName}】的属性【{string.Join(",", nullPropertyNames)}】不能为空");
            }
        }
    }

    /// <summary>
    /// 检查业务参数属性是否为null（为null抛业务异常）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="param">参数</param>
    /// <param name="paramName">参数名</param>
    /// <param name="propertyNames">属性名</param>
    public static void CheckBizParamPropertyNull<T>(this T param, string paramName, params string[]? propertyNames) where T : class
    {
        param.CheckParamNull(paramName, true);
        try
        {
            param.CheckParamPropertyNull(paramName, propertyNames);
        }
        catch (ArgumentNullException e)
        {
            throw BizException.CreateFromSysException(e);
        }
    }

#if NET6_0

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopy<T>(this T obj)
    {
        obj.CheckParamNull(nameof(obj));

        //如果是字符串或值类型则直接返回
        if (obj is string || obj!.GetType().IsValueType)
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
            return (T)retval!;
        }
    }

#endif

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopyByJson<T>(this T obj)
    {
        obj.CheckParamNull(nameof(obj));

        var options = new JsonSerializerOptions().DefaultConfig();
        var json = JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Deserialize<T>(json, options)!;
    }

    /// <summary>
    /// 深拷贝
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepCopyToByJson<T>(this object obj)
    {
        obj.CheckParamNull(nameof(obj));

        var options = new JsonSerializerOptions().DefaultConfig();
        var json = JsonSerializer.Serialize(obj, options);
        return JsonSerializer.Deserialize<T>(json, options)!;
    }

    /// <summary>
    /// 转大写
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string? ToStringUpper<T>(this T? obj)
    => obj?.ToString()?.ToUpper();

    /// <summary>
    /// 转小写
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string? ToStringLower<T>(this T? obj)
    => obj?.ToString()?.ToLower();

    /// <summary>
    /// 对象转url参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ObjectToUriParam<T>(this T obj) where T : class
    {
        obj.CheckParamNull(nameof(obj));

        return string.Join("&", GetObjectParam(obj));

        static List<string> GetObjectParam(object obj, string? propertyName = null)
        {
            var propertis = obj.GetType().GetProperties();
            var result = new List<string>();
            foreach (var property in propertis)
            {
                var name = string.IsNullOrEmpty(propertyName) ? property.Name : $"{propertyName}.{property.Name}";
                var value = property.GetValue(obj, null);

                if (value is null || value.Equals(property.PropertyType.DefaultValue()))
                {
                    continue;
                }
                if (value is IEnumerable array && value is not string)
                {
                    var i = 0;
                    foreach (var item in array)
                    {
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
    /// 对象转url参数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    public static string ObjectToUriParam(this object obj, string? paramName = null)
    {
        obj.CheckParamNull(nameof(obj));

        var uriParams = new List<string>();
        SetUriParam(uriParams, obj, paramName);
        return string.Join("&", uriParams);

        static void SetUriParam(List<string> uriParams, object? propertyValue, string? propertyName)
        {
            if (propertyValue is null)
            {
                return;
            }
            if (propertyValue is IEnumerable array && propertyValue is not string)
            {
                if (propertyName?.Length > 0)
                {
                    var i = 0;
                    foreach (var item in array)
                    {
                        if (item is null)
                        {
                            continue;
                        }
                        SetUriParam(uriParams, item, $"{propertyName}[{i}]");
                        i++;
                    }
                }
            }
            else if (propertyValue.GetType().IsValueType || propertyValue is string)
            {
                if (propertyName?.Length > 0)
                {
                    uriParams.Add($"{propertyName}={HttpUtility.UrlEncode(propertyValue.ToString())}");
                }
            }
            else
            {
                var childPropertis = propertyValue.GetType().GetProperties();
                foreach (var childProperty in childPropertis)
                {
                    var childPropertyName = string.IsNullOrEmpty(propertyName) ? childProperty.Name : $"{propertyName}.{childProperty.Name}";
                    var childPropertyValue = childProperty.GetValue(propertyValue, null);
                    SetUriParam(uriParams, childPropertyValue, childPropertyName);
                }
            }
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
    public static T CopyProperty<T>(this T fromObj, T toObj, params string[]? ignorePropertyNames)
    {
        fromObj.CheckParamNull(nameof(fromObj));
        toObj.CheckParamNull(nameof(toObj));
        if (fromObj is not string && !fromObj!.GetType().IsValueType)
        {
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
                            ignorePropertyNames?.Where(x => x is not null && x.StartsWith(ignorePropertyNameStarts)).Select(x => x[ignorePropertyNameStarts.Length..]).ToArray());
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