namespace Startly.Domain.DTOs.Startup.Pesquisar
{
    public class StartupVideoPesquisarDto
    {
        public Guid Id { get; set; }
        public Guid StartupId { get; set; }
        public string LinkVideo { get; set; } = string.Empty;
    }
}
