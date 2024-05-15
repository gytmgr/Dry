namespace Dry.EF8.EntityConfigs;

/// <summary>
/// 树状实体配置
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TTreeEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class EF8TreeEntityConfigBase<TBoundedContext, TTreeEntity, TKey> : TreeEntityConfigBase<TBoundedContext, TTreeEntity, TKey>
    where TBoundedContext : IBoundedContext
    where TTreeEntity : class, IEntity<TKey>, TBoundedContext
{
    /// <summary>
    /// 配置实体（表注释要在base调用之前配置，关系表才会有注释）
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<TTreeEntity> builder)
    {
        base.Configure(builder);

        var tableComment = builder.Metadata.GetComment();

        builder.HasMany(AncestorsExpression!).WithMany(DescendantsExpression!).UsingEntity<TreeAncestorRelation<TTreeEntity, TKey>>(
            x => AncestorWithMany(x.HasOne(y => y.Ancestor)).HasForeignKey(y => y.AncestorId).OnDelete(DeleteBehavior.Restrict),
            x => DescendantWithMany(x.HasOne(y => y.Relation)).HasForeignKey(y => y.RelationId).OnDelete(DeleteBehavior.Cascade),
            x =>
            {
                x.ToTable(TableName + "Ancestor");
                if (!string.IsNullOrEmpty(tableComment))
                {
#if NET8_0_OR_GREATER

                    x.ToTable(x => x.HasComment(tableComment + "祖先关系"));

#else

                    x.HasComment(tableComment + "祖先关系");

#endif
                }
                x.HasKey(x => new { x.RelationId, x.AncestorId });
                x.Property(x => x.RelationId).HasComment(string.IsNullOrEmpty(tableComment) ? "关系" : tableComment + "id");
                x.Property(x => x.AncestorId).HasComment("祖先id");
            });
    }
}