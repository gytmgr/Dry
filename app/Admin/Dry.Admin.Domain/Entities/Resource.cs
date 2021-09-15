using Dry.Core.Model;
using Dry.Domain.Entities;
using Dry.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dry.Admin.Domain.Entities
{
    /// <summary>
    /// 系统资源
    /// </summary>
    public class Resource : IAggregateRoot<Guid>, IAdminContext, ITree<Guid>, IHasAddTime
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 上级资源id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        #region 导航属性

        /// <summary>
        /// 上级资源
        /// </summary>
        public Resource Parent { get; set; }

        /// <summary>
        /// 下级资源
        /// </summary>
        public ICollection<Resource> Children { get; set; }

        /// <summary>
        /// 祖先资源
        /// </summary>
        public ICollection<Resource> Ancestors { get; set; }

        /// <summary>
        /// 子孙资源
        /// </summary>
        public ICollection<Resource> Descendants { get; set; }

        /// <summary>
        /// 祖先关系
        /// </summary>
        public ICollection<TreeAncestorRelation<Resource, Guid>> AncestorRelations { get; set; }

        /// <summary>
        /// 子代关系
        /// </summary>
        public ICollection<TreeAncestorRelation<Resource, Guid>> DescendantRelations { get; set; }

        /// <summary>
        /// 子孙资源
        /// </summary>
        public ICollection<ResourceItem> Items { get; set; }

        #endregion
    }
}