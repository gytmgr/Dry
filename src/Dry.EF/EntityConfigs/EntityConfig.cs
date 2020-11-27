using Dry.Domain;
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
    public abstract class EntityConfig<TBoundedContext, TEntity> : IEntityRegister<TBoundedContext>, IEntityTypeConfiguration<TEntity> where TBoundedContext : IBoundedContext where TEntity : class, IEntity, TBoundedContext
    {
        /// <summary>
        /// 表名称
        /// </summary>
        protected readonly string _tableName;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="tableName"></param>
        public EntityConfig(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// 注册实体
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void RegistTo(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        /// <summary>
        /// 配置字段
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(_tableName);
        }
    }
}