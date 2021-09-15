using Dry.Admin.Domain;
using Dry.Admin.Domain.Entities;
using Dry.Domain.Entities.ValueObjects;
using Dry.EF.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dry.Admin.EF.EntityConfigs
{
    public class ResourceConfig : TreeEntityConfig<IAdminContext, Resource, Guid>
    {
        protected override Expression<Func<Resource, IEnumerable<Resource>>> AncestorsExpression => x => x.Ancestors;

        protected override Expression<Func<Resource, IEnumerable<Resource>>> DescendantsExpression => x => x.Descendants;

        protected override Expression<Func<Resource, IEnumerable<TreeAncestorRelation<Resource, Guid>>>> AncestorRelationsExpression => x => x.AncestorRelations;

        protected override Expression<Func<Resource, IEnumerable<TreeAncestorRelation<Resource, Guid>>>> DescendantRelationsExpression => x => x.DescendantRelations;

        public override void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasComment("系统资源");

            base.Configure(builder);

            builder.Property(x => x.ParentId).HasComment("上级资源id");
            builder.Property(x => x.AddTime).HasComment("添加时间");

            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}