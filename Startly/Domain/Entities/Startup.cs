using Startly.Enumerators;

namespace Startly.Domain.Entities
{
    public class Startup
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
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public ICollection<StartupAtuacao> Atuacoes { get; set; } = [];
        public ICollection<StartupImagem> Imagens { get; set; } = [];
        public ICollection<StartupContato> Contatos { get; set; } = [];
        public ICollection<StartupVideo> Videos { get; set; } = [];
    }
}
