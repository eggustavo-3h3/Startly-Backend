using Startly.Domain.DTOs.Startup.Atualizar;
using Startly.Domain.Entities;
using Startly.Enumerators;

namespace Startly.Domain.DTOs.Startup.Pesquisar
{
    public class StartupPesquisarDto
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

        public List<StartupAtuacaoPesquisarDto> Atuacoes { get; set; } = [];
        public List<StartupVideoPesquisarDto> Videos { get; set; } = [];
        public List<StartupImagemPesquisarDto> Imagens { get; set; } = [];
        public List<StartupContatoPesquisarDto> Contatos { get; set; } = [];

    }
}
