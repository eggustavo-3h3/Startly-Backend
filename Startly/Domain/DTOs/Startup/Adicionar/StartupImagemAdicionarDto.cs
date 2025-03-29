using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupImagemAdicionarDto
    {
        public EnumTipoImagem TipoImagem { get; set; }
        public string Imagem { get; set; } = string.Empty;
    }
}
