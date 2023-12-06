namespace Dry.EF.Repositories;

/// <summary>
/// ef只读仓储
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class ReadOnlyRepository<TEntity> : ReadOnlyRepositoryBase<TEntity>, IDependency<IReadOnlyRepository<TEntity>> where TEntity : class, IEntity, IBoundedContext
{
    /// <summary>
    /// 构造体
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ReadOnlyRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}