using Dry.Admin.Domain;
using Dry.Admin.Domain.Entities;
using Dry.EF.EntityConfigs;
using Dry.EF.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dry.Admin.EF.EntityConfigs
{
    internal sealed class ApplicationConfig : EntityConfig<IAdminContext, Application>
    {
        public ApplicationConfig() : base(nameof(Application)) { }

        public override void Configure(EntityTypeBuilder<Application> builder)
        {
            base.Configure(builder);
            builder.HasDescription("审核流程");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(50).HasDescription("系统id");
            builder.Property(x => x.Type).HasDescription("类型");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasDescription("名称");
            builder.Property(x => x.Secret).IsRequired().HasMaxLength(200).HasDescription("Secret");
            builder.Property(x => x.Url).HasDescription("地址");
            builder.Property(x => x.Description).HasDescription("说明");
            builder.Property(x => x.Enable).HasDescription("是否可用");
            builder.Property(x => x.AddTime).HasDescription("添加时间");

            builder.HasIndex(x => x.AddTime);
        }
    }
}