namespace Dry.Core.Utilities;

/// <summary>
/// 类型扩展
/// </summary>
public static class TypeExtension
{
    /// <summary>
    /// 判断"type"指定的类型是否继承自"pattern"指定的类型，或实现了"pattern"指定的接口
    /// 支持未知类型参数的泛型，如typeof(List&lt;&gt;)
    /// </summary>
    /// <param name="type">需要测试的类型</param>
    /// <param name="pattern">要匹配的类型，如 typeof(int)，typeof(IEnumerable)，typeof(List&lt;&gt;)，typeof(List&lt;int&gt;)，typeof(IDictionary&lt;,&gt;)</param>
    /// <returns></returns>
    public static bool IsDerivedFrom(this Type type, Type pattern)
    {
        type.CheckParamNull(nameof(type));
        pattern.CheckParamNull(nameof(pattern));

        // 测试非泛型类型（如ArrayList）或确定类型参数的泛型类型（如List<int>，类型参数T已经确定为int）
        if (type.IsSubclassOf(pattern))
        {
            return true;
        }

        // 测试非泛型接口（如IEnumerable）或确定类型参数的泛型接口（如IEnumerable<int>，类型参数T已经确定为int）
        if (pattern.IsAssignableFrom(type))
        {
            return true;
        }

        // 测试泛型接口（如IEnumerable<>，IDictionary<,>，未知类型参数，留空）
        if (type.GetInterfaces().Any(IsTheRawGenericType))
        {
            return true;
        }

        // 测试泛型类型（如List<>，Dictionary<,>，未知类型参数，留空）
        Type? currentType = type;
        while (currentType is not null && type != typeof(object))
        {
            if (IsTheRawGenericType(currentType))
            {
                return true;
            }
            currentType = currentType.BaseType;
        }

        // 没有找到任何匹配的接口或类型。
        return false;

        // 测试某个类型是否是指定的原始接口。
        bool IsTheRawGenericType(Type test)
            => pattern == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
    }

    /// <summary>
    /// 获取类型默认值
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object? DefaultValue(this Type type)
    {
        type.CheckParamNull(nameof(type));
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    /// <summary>
    /// 获取指定类型指定字段类型的数据
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    /// <param name="classType"></param>
    /// <returns></returns>
    public static TField[] GetFields<TField>(this Type classType)
    {
        classType.CheckParamNull(nameof(classType));
        return classType.GetFields().Where(x => x.GetValue(null) is not null && x.GetValue(null)!.GetType() == typeof(TField)).Select(x => (TField)x.GetValue(null)!).ToArray();
    }
}