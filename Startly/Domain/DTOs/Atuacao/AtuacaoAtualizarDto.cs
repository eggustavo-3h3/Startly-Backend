namespace Startly.Domain.DTOs.Atuacao
{
    public class AtuacaoAtualizarDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}