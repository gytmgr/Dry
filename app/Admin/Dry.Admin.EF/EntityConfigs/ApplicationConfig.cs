using Dry.Admin.Domain;
using Dry.Admin.Domain.Entities;
using Dry.EF.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dry.Admin.EF.EntityConfigs
{
    internal sealed class ApplicationConfig : EntityConfig<IAdminContext, Application, string>
    {
        protected override string TableComment => "应用";

        public override void Configure(EntityTypeBuilder<Application> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Id).HasMaxLength(50);
            builder.Property(x => x.Type).HasComment("类型");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50).HasComment("名称");
            builder.Property(x => x.Secret).IsRequired().HasMaxLength(200).HasComment("Secret");
            builder.Property(x => x.Url).HasComment("地址");
            builder.Property(x => x.Description).HasComment("说明");
            builder.Property(x => x.Enable).HasComment("是否可用");
            builder.Property(x => x.AddTime).HasComment("添加时间");

            builder.HasIndex(x => x.AddTime);
        }
    }
}