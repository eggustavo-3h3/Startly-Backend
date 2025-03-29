using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupImagemAtualizarDto
    {
        public Guid StartupId { get; set; }
        public string Imagem { get; set; } = string.Empty;
        public EnumTipoImagem TipoImagem { get; set; }

    }
}
