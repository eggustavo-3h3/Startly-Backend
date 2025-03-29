using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Pesquisar
{
    public class StartupImagemPesquisarDto
    {
        public Guid id { get; set; }
        public EnumTipoImagem TipoImagem { get; set; }
        public string Imagem { get; set; } = string.Empty;
    }
}
