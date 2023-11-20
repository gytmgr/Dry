namespace Dry.EF.Extensions;

/// <summary>
/// 数据库说明扩展
/// </summary>
public static class DbDescriptionExtension
{
    /// <summary>
    /// 表说明
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entityTypeBuilder"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static EntityTypeBuilder<TEntity> HasDescription<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string description) where TEntity : class
        => entityTypeBuilder.HasAnnotation(ModelBuilderExtension.DbDescriptionAnnotationName, description);

    /// <summary>
    /// 表说明
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDependentEntity"></typeparam>
    /// <param name="ownedNavigationBuilder"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static OwnedNavigationBuilder<TEntity, TDependentEntity> HasDescription<TEntity, TDependentEntity>(this OwnedNavigationBuilder<TEntity, TDependentEntity> ownedNavigationBuilder, string description) where TEntity : class where TDependentEntity : class
        => ownedNavigationBuilder.HasAnnotation(ModelBuilderExtension.DbDescriptionAnnotationName, description);

    /// <summary>
    /// 列说明
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="propertyBuilder"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static PropertyBuilder<TProperty> HasDescription<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, string description)
        => propertyBuilder.HasAnnotation(ModelBuilderExtension.DbDescriptionAnnotationName, description);
}