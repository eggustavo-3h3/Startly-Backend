using FluentValidation;

namespace Startly.Domain.DTOs.Atuacao
{
    public class AtuacaoAtualizarDtoValidator : AbstractValidator<AtuacaoAtualizarDto>
    {
        public AtuacaoAtualizarDtoValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Descrição deve ser preenchida Obrigatoriamente")
                .MaximumLength(200).WithMessage("Descrição deve ter no máximo ");
        }
    }
}
