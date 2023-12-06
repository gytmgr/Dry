namespace Dry.EF.EntityConfigs;

/// <summary>
/// 定义将实体配置类注册到上下文中
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
public interface IEntityRegister<TBoundedContext> : IDependency<IEntityRegister<TBoundedContext>> where TBoundedContext : IBoundedContext
{
    /// <summary>
    /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
    /// </summary>
    /// <param name="modelBuilder">实体映射配置注册器</param>
    void RegistTo(ModelBuilder modelBuilder);
}