namespace Dry.Core.Utilities;

/// <summary>
/// linq帮助类
/// </summary>
public static class LinqHelper
{
    /// <summary>
    /// 获取表达式
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="keyName"></param>
    /// <returns></returns>
    private static DryData<(Expression Body, ParameterExpression Param)>? GetExpressionInfo<TSource>(string keyName)
    {
        var type = typeof(TSource);
        var param = Expression.Parameter(type);
        var propertyNames = keyName.Split(".");
        Expression propertyAccess = param;
        foreach (var propertyName in propertyNames)
        {
            var property = type.GetProperty(propertyName);
            if (property is null)
            {
                return null;
            }
            type = property.PropertyType;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
        }
        return new DryData<(Expression, ParameterExpression)> { Data = (propertyAccess, param) };
    }

    /// <summary>
    /// 获取根据字段名获取Lambda表达式
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public static Expression<Func<TSource, TProperty>>? GetKeySelector<TSource, TProperty>(string keyName)
    {
        keyName.CheckParamNull(nameof(keyName));

        var expressionInfo = GetExpressionInfo<TSource>(keyName!);
        if (expressionInfo is null)
        {
            return null;
        }
        return Expression.Lambda<Func<TSource, TProperty>>(expressionInfo.Data.Body, expressionInfo.Data.Param);
    }
}