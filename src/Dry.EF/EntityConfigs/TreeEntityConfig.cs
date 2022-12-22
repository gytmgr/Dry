namespace Dry.EF.EntityConfigs;

/// <summary>
/// 树状实体配置
/// </summary>
/// <typeparam name="TBoundedContext"></typeparam>
/// <typeparam name="TTreeEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class TreeEntityConfig<TBoundedContext, TTreeEntity, TKey> : EntityConfig<TBoundedContext, TTreeEntity, TKey>
    where TBoundedContext : IBoundedContext
    where TTreeEntity : class, IEntity<TKey>, TBoundedContext
{
    /// <summary>
    /// 实体祖先属性表达式
    /// </summary>
    protected abstract Expression<Func<TTreeEntity, IEnumerable<TTreeEntity>>> AncestorsExpression { get; }

    /// <summary>
    /// 实体子孙属性表达式
    /// </summary>
    protected abstract Expression<Func<TTreeEntity, IEnumerable<TTreeEntity>>> DescendantsExpression { get; }

    /// <summary>
    /// 实体祖先关系属性表达式
    /// </summary>
    protected virtual Expression<Func<TTreeEntity, IEnumerable<TreeAncestorRelation<TTreeEntity, TKey>>>> AncestorRelationsExpression => null;

    /// <summary>
    /// 实体子孙关系属性表达式
    /// </summary>
    protected virtual Expression<Func<TTreeEntity, IEnumerable<TreeAncestorRelation<TTreeEntity, TKey>>>> DescendantRelationsExpression => null;

    /// <summary>
    /// 实体祖先和关系实体的对多配置
    /// </summary>
    /// <param name="hasOne"></param>
    /// <returns></returns>
    private ReferenceCollectionBuilder<TTreeEntity, TreeAncestorRelation<TTreeEntity, TKey>> AncestorWithMany(ReferenceNavigationBuilder<TreeAncestorRelation<TTreeEntity, TKey>, TTreeEntity> hasOne)
    {
        if (AncestorRelationsExpression is null)
        {
            return hasOne.WithMany();
        }
        else
        {
            return hasOne.WithMany(AncestorRelationsExpression);
        }
    }

    /// <summary>
    /// 实体子孙和关系实体的对多配置
    /// </summary>
    /// <param name="hasOne"></param>
    /// <returns></returns>
    private ReferenceCollectionBuilder<TTreeEntity, TreeAncestorRelation<TTreeEntity, TKey>> DescendantWithMany(ReferenceNavigationBuilder<TreeAncestorRelation<TTreeEntity, TKey>, TTreeEntity> hasOne)
    {
        if (DescendantRelationsExpression is null)
        {
            return hasOne.WithMany();
        }
        else
        {
            return hasOne.WithMany(DescendantRelationsExpression);
        }
    }

    /// <summary>
    /// 配置实体（表注释要在base调用之前配置，关系表才会有注释）
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<TTreeEntity> builder)
    {
        base.Configure(builder);

        string tableComment = builder.Metadata.GetComment();

        builder.HasMany(AncestorsExpression).WithMany(DescendantsExpression).UsingEntity<TreeAncestorRelation<TTreeEntity, TKey>>(
            x => AncestorWithMany(x.HasOne(y => y.Ancestor)).HasForeignKey(y => y.AncestorId).OnDelete(DeleteBehavior.Restrict),
            x => DescendantWithMany(x.HasOne(y => y.Relation)).HasForeignKey(y => y.RelationId).OnDelete(DeleteBehavior.Cascade),
            x =>
            {
                x.ToTable(TableName + "Ancestor");
                if (!string.IsNullOrEmpty(tableComment))
                {
                    x.HasComment(tableComment + "祖先关系");
                }
                x.HasKey(x => new { x.RelationId, x.AncestorId });
                x.Property(x => x.RelationId).HasComment(string.IsNullOrEmpty(tableComment) ? "关系" : tableComment + "id");
                x.Property(x => x.AncestorId).HasComment("祖先id");
            });
    }
}