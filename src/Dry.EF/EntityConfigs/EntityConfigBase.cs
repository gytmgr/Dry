namespace Dry.EF.EntityConfigs;

/// <summary>
/// 实体配置
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract class EntityConfigBase<TBoundedContext, TEntity> : IEntityRegister<TBoundedContext>, IEntityTypeConfiguration<TEntity>
    where TBoundedContext : IBoundedContext
    where TEntity : class, IEntity, TBoundedContext
{
    /// <summary>
    /// 表名称
    /// </summary>
    protected virtual string TableName => typeof(TEntity).Name;

    /// <summary>
    /// 注册实体
    /// </summary>
    /// <param name="modelBuilder"></param>
    public virtual void RegistTo(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfiguration(this);

    /// <summary>
    /// 配置表信息
    /// </summary>
    /// <param name="builder"></param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(TableName);
        var entityType = typeof(TEntity);
        if (entityType.IsDerivedFrom(typeof(IHasCode)))
        {
            builder.Property(LinqHelper.GetKeySelector<TEntity, string>(nameof(IHasCode.Code))!).IsRequired().HasMaxLength(50).HasComment("编码");
        }
        if (entityType.IsDerivedFrom(typeof(IHasName)))
        {
            builder.Property(LinqHelper.GetKeySelector<TEntity, string>(nameof(IHasName.Name))!).IsRequired().HasMaxLength(50).HasComment("名称");
        }
        if (entityType.IsDerivedFrom(typeof(IHasOrder)))
        {
            builder.Property(LinqHelper.GetKeySelector<TEntity, int>(nameof(IHasOrder.Order))!).HasComment("排序");
        }
        if (entityType.IsDerivedFrom(typeof(IHasAddTime)))
        {
            builder.Property(LinqHelper.GetKeySelector<TEntity, DateTime>(nameof(IHasAddTime.AddTime))!).HasComment("添加时间");
        }
        if (entityType.IsDerivedFrom(typeof(IHasUpdateTime)))
        {
            builder.Property(LinqHelper.GetKeySelector<TEntity, DateTime?>(nameof(IHasUpdateTime.UpdateTime))!).HasComment("更新时间");
        }
    }
}

/// <summary>
/// 主键实体配置
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class EntityConfigBase<TBoundedContext, TEntity, TKey> : EntityConfigBase<TBoundedContext, TEntity>
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