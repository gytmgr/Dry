﻿namespace Dry.Domain.Entities
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 单一主键实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }
    }
}