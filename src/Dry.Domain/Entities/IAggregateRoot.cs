namespace Dry.Domain.Entities;

/// <summary>
/// 聚合根
/// </summary>
public interface IAggregateRoot : IEntity
{
}

/// <summary>
/// 单一主键聚合根
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
{
}