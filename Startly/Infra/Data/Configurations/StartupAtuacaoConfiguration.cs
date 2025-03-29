using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class StartupAtuacaoConfigurantion : IEntityTypeConfiguration<StartupAtuacao>
    {
        public void Configure(EntityTypeBuilder<StartupAtuacao> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(a => a.StartupId)
              .IsRequired();

            builder.Property(a => a.AtuacaoId)
              .IsRequired();

            builder.ToTable("TB_StartupAtuacao");
        }
    }
}

