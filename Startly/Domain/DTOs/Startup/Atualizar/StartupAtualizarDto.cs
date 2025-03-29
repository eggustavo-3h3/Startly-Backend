using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupAtualizarDto
    {
        public Guid id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string metas { get; set; } = string.Empty;
        public int QuantidadeFuncionario { get; set; }
        public EnumTicketMedio EnumTicket { get; set; }
        public EnumTipoDeAtendimento EnumTipoDeAtendimento { get; set; }
        public List<StartupAtuacaoAtualizarDto> Atuacoes { get; set; } = [];
        public List<StartupVideoAtualizarDto> Videos { get; set; } = [];
        public List<StartupImagemAtualizarDto> Imagens { get; set; } = [];
        public List<StartupContatoAtualizarDto> Contatos { get; set; } = [];

    }
}
