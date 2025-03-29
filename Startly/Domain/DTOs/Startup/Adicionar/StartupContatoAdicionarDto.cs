using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupContatoAdicionarDto
    {
        public EnumTipoContato Contato { get; set; }
        public string Conteudo { get; set; } = string.Empty;
    }
}
