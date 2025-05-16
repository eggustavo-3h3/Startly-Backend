using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Startly.Domain.Entities;

namespace Startly.Infra.Data.Configurations
{
    public class StartupConfiguration : IEntityTypeConfiguration<Startup>
    {
        public void Configure(EntityTypeBuilder<Startup> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.Metas)
                .IsRequired()
                .HasMaxLength(3000);

            builder.Property(p => p.CNPJ)
                .IsRequired(false)
                .HasMaxLength(14);

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(9);

            builder.Property(p => p.Logradouro)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(p => p.Bairro)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Municipio)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.UF)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(p => p.SiteStartup)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(p => p.QuantidadeFuncionario)
                .IsRequired();

            builder.Property(p => p.EnumTicket)
                 .HasConversion<string>()
                 .IsRequired();

            builder.Property(p => p.EnumTipoDeAtendimento)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.ResponsavelCadastro)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(p => p.Login)
                 .IsRequired()
                 .HasMaxLength(100);

            builder.Property(p => p.Senha)
                 .IsRequired();

            builder.ToTable("TB_Startup");

        }
    }
}
