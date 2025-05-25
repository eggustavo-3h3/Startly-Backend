using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class StartupVideoConfiguration : IEntityTypeConfiguration<StartupVideo>
    {
        public void Configure(EntityTypeBuilder<StartupVideo> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.LinkVideo)
                .IsRequired();

            builder.Property(v => v.StartupId)
                .IsRequired();

            builder.ToTable("TB_StartupVideo");
        }
    }
}
