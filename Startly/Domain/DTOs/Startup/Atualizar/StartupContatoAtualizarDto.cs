using Startly.Enumerators;
using System.Diagnostics;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupContatoAtualizarDto
    {
        public Guid StartupId { get; set; }
        public EnumTipoContato Contato { get; set; }
        public string Conteudo { get; set; } = string.Empty;

    }
}
