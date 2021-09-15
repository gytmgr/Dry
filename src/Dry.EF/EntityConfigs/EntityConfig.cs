﻿using Dry.Domain;
using Dry.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dry.EF.EntityConfigs
{
    /// <summary>
    /// 实体配置
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityConfig<TBoundedContext, TEntity> : IEntityRegister<TBoundedContext>, IEntityTypeConfiguration<TEntity>
        where TBoundedContext : IBoundedContext
        where TEntity : class, IEntity, TBoundedContext
    {
        /// <summary>
        /// 表名称
        /// </summary>
        protected virtual string TableName => typeof(TEntity).Name;

        /// <summary>
        /// 表注释
        /// </summary>
        protected virtual string TableComment => null;

        /// <summary>
        /// 注册实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        public virtual void RegistTo(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        /// <summary>
        /// 配置表信息
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName);
            if (!string.IsNullOrEmpty(TableComment))
            {
                builder.HasComment(TableComment);
            }
        }
    }

    /// <summary>
    /// 主键实体配置
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityConfig<TBoundedContext, TEntity, TKey> : EntityConfig<TBoundedContext, TEntity>
        where TBoundedContext : IBoundedContext
        where TEntity : class, IEntity<TKey>, TBoundedContext
    {
        /// <summary>
        /// 配置主键信息
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasComment("系统id");
        }
    }
}