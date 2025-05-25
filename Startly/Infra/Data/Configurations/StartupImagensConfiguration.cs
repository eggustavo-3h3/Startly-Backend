
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class StartupImagensConfigurantion : IEntityTypeConfiguration<StartupImagem>
    {
        public void Configure(EntityTypeBuilder<StartupImagem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.StartupId)
               .IsRequired();

            builder.Property(i => i.Imagem)
                .IsRequired();

            builder.ToTable("TB_StartupImagens");
        }
    }
}
