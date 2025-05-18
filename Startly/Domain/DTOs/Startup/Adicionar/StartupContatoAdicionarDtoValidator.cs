using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupContatoAdicionarDtoValidator : AbstractValidator<StartupContatoAdicionarDto>
    {
        public StartupContatoAdicionarDtoValidator()
        {
            RuleFor(p => p.Contato)
                .NotEmpty().WithMessage("O campo Contato não pode estar vazio");

            RuleFor(p => p.Conteudo)
                .NotEmpty().WithMessage("O campo Conteudo não pode estar vazio")
                .MaximumLength(300).WithMessage("O campo Conteudo deve ter no máximo 300 caracteres");
        }
    }
}
