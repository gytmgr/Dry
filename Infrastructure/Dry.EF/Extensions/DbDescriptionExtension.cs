using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Dry.EF.Extensions
{
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
        public static EntityTypeBuilder<TEntity> HasDescription<TEntity>([NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder, [NotNull] string description) where TEntity : class
        {
            return entityTypeBuilder.HasAnnotation(ModelBuilderExtension.DbDescriptionAnnotationName, description);
        }

        /// <summary>
        /// 列说明
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyBuilder"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static PropertyBuilder<TProperty> HasDescription<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder, [NotNull] string description)
        {
            return propertyBuilder.HasAnnotation(ModelBuilderExtension.DbDescriptionAnnotationName, description);
        }
    }
}