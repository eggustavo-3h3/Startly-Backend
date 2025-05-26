using Startly.Domain.Enumerators;

namespace Startly.Domain.DTOs.Startup.Obter
{
    public class StartupObterDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Metas { get; set; } = string.Empty;
        public string? CNPJ { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string? SiteStartup { get; set; }
        public int QuantidadeFuncionario { get; set; }
        public EnumTicketMedio EnumTicket { get; set; }
        public EnumTipoDeAtendimento EnumTipoDeAtendimento { get; set; }
        public string ResponsavelCadastro { get; set; } = string.Empty;
        public List<StartupAtuacaoObterDto> Atuacoes { get; set; } = [];
        public List<StartupVideoObterDto> Videos { get; set; } = [];
        public List<StartupImagemObterDto> Imagens { get; set; } = [];
        public List<StartupContatoObterDto> Contatos { get; set; } = [];

    }
}
