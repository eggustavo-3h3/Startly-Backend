namespace Startly.Domain.Entities
{
    public class StartupVideo
    {
        public Guid Id { get; set; }
        public Guid StartupId { get; set; }
        public string LinkVideo { get; set; } = string.Empty;
        public Startup Startup { get; set; }
    }
}
