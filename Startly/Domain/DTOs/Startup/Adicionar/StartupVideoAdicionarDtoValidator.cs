using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupVideoAdicionarDtoValidator : AbstractValidator<StartupVideoAdicionarDto>
    {
        public StartupVideoAdicionarDtoValidator()
        {
            RuleFor(p => p.LinkVideo)
                .MaximumLength(200).WithMessage("O campo Video deve ter no máximo 200 caracteres");
        }

    }
}
