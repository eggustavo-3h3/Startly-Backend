using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupVideoAdicionarDtoValidator : AbstractValidator<StartupVideoAdicionarDto>
    {
        public StartupVideoAdicionarDtoValidator()
        {
            RuleFor(p => p.LinkVideo)
                .NotEmpty().WithMessage("O campo LinkVideo não pode estar vazio");
        }
    }
}
