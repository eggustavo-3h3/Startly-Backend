using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class AtuacaoConfiguration : IEntityTypeConfiguration<Atuacao>
    {
        public void Configure(EntityTypeBuilder<Atuacao> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.ToTable("TB_Atuacao");
        }
    }
}

