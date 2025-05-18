using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupVideoAtualizarDtoValidator : AbstractValidator<StartupVideoAtualizarDto>
    {
        public StartupVideoAtualizarDtoValidator()
        {
          RuleFor(p => p.LinkVideo)
             .MaximumLength(200).WithMessage("O campo Video deve ter no máximo 200 caracteres");
        }
    }
}
