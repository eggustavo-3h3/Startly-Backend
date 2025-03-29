using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class StartupContatoConfigurantion : IEntityTypeConfiguration<StartupContato>
    {
        public void Configure(EntityTypeBuilder<StartupContato> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.StartupId)
             .IsRequired();

            builder.Property(t => t.Contato)
             .HasConversion<string>()
             .IsRequired();

            builder.Property(t => t.Conteudo)
              .IsRequired()
              .HasMaxLength(300);

            builder.ToTable("TB_StartupContato");
        }
    }
}

