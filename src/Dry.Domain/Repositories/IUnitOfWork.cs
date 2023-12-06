namespace Dry.Domain.Repositories;

/// <summary>
/// 工作单元
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// 提交
    /// </summary>
    /// <returns></returns>
    Task<int> CompleteAsync();
}

/// <summary>
/// 工作单元
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public interface IUnitOfWork<TBoundedContext> : IUnitOfWork, IDependency<IUnitOfWork<TBoundedContext>> where TBoundedContext : IBoundedContext
{
}