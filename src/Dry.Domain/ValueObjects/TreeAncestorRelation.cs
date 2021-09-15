namespace Dry.Domain.Entities.ValueObjects
{
    /// <summary>
    /// 树状实体与祖先关系
    /// </summary>
    /// <typeparam name="TTreeEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public record TreeAncestorRelation<TTreeEntity, TKey> where TTreeEntity : IEntity<TKey>
    {
        /// <summary>
        /// 树状实体id
        /// </summary>
        public TKey RelationId { get; init; }

        /// <summary>
        /// 祖先id
        /// </summary>
        public TKey AncestorId { get; init; }

        #region 导航属性

        /// <summary>
        /// 树状实体
        /// </summary>
        public TTreeEntity Relation { get; init; }

        /// <summary>
        /// 祖先
        /// </summary>
        public TTreeEntity Ancestor { get; init; }

        #endregion
    }
}