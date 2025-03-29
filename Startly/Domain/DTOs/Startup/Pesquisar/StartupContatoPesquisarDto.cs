using Startly.Enumerators;
using System.Diagnostics;

namespace Startly.Domain.DTOs.Startup.Pesquisar
{
    public class StartupContatoPesquisarDto
    {
        public Guid Id { get; set; }
        public Guid StartupId { get; set; }
        public EnumTipoContato Contato { get; set; }

    }
}
