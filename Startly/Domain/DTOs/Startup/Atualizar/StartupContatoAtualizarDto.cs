using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupContatoAtualizarDto
    {
        public EnumTipoContato Contato { get; set; }
        public string Conteudo { get; set; } = string.Empty;
    }
}
