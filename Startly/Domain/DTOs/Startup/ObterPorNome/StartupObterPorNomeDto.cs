using Startly.Domain.Enumerators;

namespace Startly.Domain.DTOs.Startup.Obter
{
    public class StartupObterPorNomeDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public List<StartupAtuacaoObterNomeDto> Atuacoes { get; set; } = [];

    }
}
