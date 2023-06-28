namespace Dry.Core.Utilities;

/// <summary>
/// Lambda表达式扩展
/// </summary>
public static class ExpressionExtension
{
    /// <summary>
    /// 条件合并_并集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression1"></param>
    /// <param name="expression2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        if (expression1 is null)
        {
            return expression2;
        }
        if (expression2 is null)
        {
            return expression1;
        }
        var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
    }

    /// <summary>
    /// 条件合并_交集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expression1"></param>
    /// <param name="expression2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        if (expression1 is null)
        {
            return expression2;
        }
        if (expression2 is null)
        {
            return expression1;
        }
        var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
    }
}