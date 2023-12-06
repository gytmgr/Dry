namespace Dry.Admin.EF.EntityConfigs;

internal sealed class ResourceItemConfig : EntityConfigBase<IAdminContext, ResourceItem>, IDependency<IEntityRegister<IAdminContext>>
{
    public override void Configure(EntityTypeBuilder<ResourceItem> builder)
    {
        base.Configure(builder);

        builder.HasComment("资源项");

        builder.HasKey(x => new { x.ResourceId, x.Type });

        builder.Property(x => x.ResourceId).HasComment("资源id");
        builder.Property(x => x.Type).HasComment("资源项类型");
        builder.Property(x => x.AddTime).HasComment("添加时间");

        builder.HasOne(x => x.Resource).WithMany(x => x.Items).HasForeignKey(x => x.ResourceId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.AddTime);
    }
}