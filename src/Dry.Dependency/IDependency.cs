using System;

namespace Dry.Dependency
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependency
    {
        /// <summary>
        /// 根服务引擎
        /// </summary>
        public static IServiceProvider RootServiceProvider { get; set; }
    }

    /// <summary>
    /// 域范围依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDependency<T> : IDependency
    {
    }

    /// <summary>
    /// 瞬时依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransientDependency<T> : IDependency
    {
    }

    /// <summary>
    /// 单例依赖注入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingletonDependency<T> : IDependency
    {
    }
}