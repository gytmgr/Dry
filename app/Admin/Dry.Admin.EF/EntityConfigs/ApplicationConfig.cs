namespace Dry.Admin.EF.EntityConfigs;

internal sealed class ApplicationConfig : EntityConfigBase<IAdminContext, Application, string>, IDependency<IEntityRegister<IAdminContext>>
{
    public override void Configure(EntityTypeBuilder<Application> builder)
    {
        base.Configure(builder);

        builder.HasComment("应用");

        builder.Property(x => x.Id).HasMaxLength(50);
        builder.Property(x => x.Type).HasComment($"类型（{EnumHelper.GetDescription<ApplicationType>()}）");
        builder.Property(x => x.Secret).IsRequired().HasMaxLength(200).HasComment("Secret");
        builder.Property(x => x.Url).HasComment("地址");
        builder.Property(x => x.Description).HasComment("说明");
        builder.Property(x => x.Enable).HasComment("是否可用");

        builder.HasIndex(x => x.AddTime);
    }
}