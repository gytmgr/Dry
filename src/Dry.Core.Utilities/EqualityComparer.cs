namespace Dry.Core.Utilities;

/// <summary>
/// 忽略HashCode比较
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class IgnoreHashCodeEqualityComparer<T> : EqualityComparer<T>
{
    /// <summary>
    /// 返回默认值以忽略HashCode比较
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override int GetHashCode([DisallowNull] T obj)
        => default;
}

/// <summary>
/// 有唯一标识属性类型比较
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TId"></typeparam>
public class HasIdEqualityComparer<T, TId> : IgnoreHashCodeEqualityComparer<T> where T : IHasId<TId>
{
    /// <summary>
    /// 获取默认值
    /// </summary>
    public static new EqualityComparer<T> Default => new HasIdEqualityComparer<T, TId>();

    /// <summary>
    /// 通过id是否相等比较
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override bool Equals(T? x, T? y)
    {
        if (x is null && y is null)
        {
            return true;
        }
        if (x is null || y is null)
        {
            return false;
        }
        return x.Id?.Equals(y.Id) ?? false;
    }
}

/// <summary>
/// 自定义比较
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomEqualityComparer<T> : IgnoreHashCodeEqualityComparer<T>
{
    /// <summary>
    /// 比较方法
    /// </summary>
    public Func<T?, T?, bool>? EqualFunc { private get; set; }

    /// <summary>
    /// 获取默认值
    /// </summary>
    public static new EqualityComparer<T> Default => new CustomEqualityComparer<T>();

    /// <summary>
    /// 构造器
    /// </summary>

    public CustomEqualityComparer() { }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="equalFunc">比较方法</param>
    public CustomEqualityComparer(Func<T?, T?, bool> equalFunc)
        => EqualFunc = equalFunc;

    /// <summary>
    /// 调用初始化比较方法比较
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public override bool Equals(T? x, T? y)
        => EqualFunc?.Invoke(x, y) ?? false;
}