using Dry.Domain;
using Dry.EF.EntityConfigs;
using Dry.EF.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dry.EF.Contexts
{
    /// <summary>
    /// ef上下文
    /// </summary>
    /// <typeparam name="TBoundedContext"></typeparam>
    public abstract class DryDbContext<TBoundedContext> : DbContext, IBoundedContext where TBoundedContext : IBoundedContext
    {
        /// <summary>
        /// 实体配置信息
        /// </summary>
        private readonly IEnumerable<IEntityRegister<TBoundedContext>> _entityRegisters;

        /// <summary>
        /// 构造体
        /// </summary>
        /// <param name="options"></param>
        /// <param name="entityRegisters"></param>
        public DryDbContext(DbContextOptions options, IEnumerable<IEntityRegister<TBoundedContext>> entityRegisters) : base(options)
        {
            _entityRegisters = entityRegisters;
        }

        /// <summary>
        /// 注册实体配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityRegister in _entityRegisters)
            {
                entityRegister.RegistTo(modelBuilder);
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigDatabaseDescription();
        }
    }
}