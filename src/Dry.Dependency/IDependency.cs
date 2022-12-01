namespace Dry.Dependency
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDependency<T>
    {
    }

    /// <summary>
    /// 瞬时依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransientDependency<T>
    {
    }

    /// <summary>
    /// 单例依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingletonDependency<T>
    {
    }
}