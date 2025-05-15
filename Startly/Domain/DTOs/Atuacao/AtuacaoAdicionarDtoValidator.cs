using FluentValidation;

namespace Startly.Domain.DTOs.Atuacao
{
    public class AtuacaoAdicionarDTOValidator : AbstractValidator<AtuacaoAdicionarDto>
    {
        public AtuacaoAdicionarDTOValidator()
        {
            //RuleFor(p => p.Descricao)
                //.NotEmpty().WithMessage("Descrição deve ser preenchida Obrigatoriamente")
                //.MaximumLength().WithMessage("Descrição deve ter no máximo ");
        }
    }
}
