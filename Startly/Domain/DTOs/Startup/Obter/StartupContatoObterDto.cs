using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Obter
{
    public class StartupContatoObterDto
    {
        public EnumTipoContato TipoContato { get; set; }
        public string Conteudo { get; set; } = string.Empty;
    }
}
