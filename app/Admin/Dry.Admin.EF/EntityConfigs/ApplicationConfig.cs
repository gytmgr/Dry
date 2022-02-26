using Dry.Admin.Domain;
using Dry.Admin.Domain.Entities;
using Dry.Admin.Domain.Shared.Enums;
using Dry.Core.Utilities;
using Dry.EF.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dry.Admin.EF.EntityConfigs
{
    internal sealed class ApplicationConfig : EntityConfig<IAdminContext, Application, string>
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
}