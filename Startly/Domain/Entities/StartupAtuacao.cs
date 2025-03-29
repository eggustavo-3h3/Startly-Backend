namespace Startly.Domain.Entities
{
    public class StartupAtuacao
    {
        public Guid Id { get; set; }
        public Guid StartupId { get; set; }
        public Guid AtuacaoId { get; set; }
        public  Startup Startup { get; set; }
        public  Atuacao Atuacao { get; set; }
    }
}
