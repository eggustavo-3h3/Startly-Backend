namespace Startly.Domain.DTOs.Startup.Listar
{
    public class StartupListarDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<StartupAtuacaoListarDto> Atuacoes { get; set; } = [];
        public List<StartupImagemListarDto> Imagens { get; set; } = [];
    }
}
