using Startly.Domain.Enumerators;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupAtualizarDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Metas { get; set; } = string.Empty;
        public string? EmailPessoal { get; set; }
        public string EmailCorporativo { get; set; } = string.Empty;
        public string TelefoneFixo { get; set; } = string.Empty;
        public string? LinkedIn { get; set; }
        public int QuantidadeFuncionario { get; set; }
        public EnumTicketMedio TicketMedio { get; set; }
        public EnumTipoDeAtendimento TipoAtendimento { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string? UrlVideo { get; set; }
        public List<StartupAtuacaoAtualizarDto> Atuacoes { get; set; } = [];
        public List<StartupImagemAtualizarDto> Imagens { get; set; } = [];

    }
}
