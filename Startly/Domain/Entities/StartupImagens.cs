using Startly.Enumerators;

namespace Startly.Domain.Entities
{
    public class StartupImagem
    {
        public Guid Id { get; set; }
        public Guid StartupId { get; set; }
        public EnumTipoImagem TipoImagem { get; set; }
        public string Imagem { get; set; } = string.Empty;
        public Startup Startup { get; set; }
    }
}
