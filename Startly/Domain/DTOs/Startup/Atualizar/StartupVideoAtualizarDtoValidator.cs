using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupVideoAtualizarDtoValidator : AbstractValidator<StartupVideoAtualizarDto>
    {
        public StartupVideoAtualizarDtoValidator()
        {
            RuleFor(p => p.LinkVideo)
                .NotEmpty().WithMessage("O campo LinkVideo não pode estar vazio");
        }
    }
}
