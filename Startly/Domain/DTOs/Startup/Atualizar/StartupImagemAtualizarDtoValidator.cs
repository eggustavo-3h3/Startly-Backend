using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupImagemAtualizarDtoValidator : AbstractValidator<StartupImagemAtualizarDto>
    {
        public StartupImagemAtualizarDtoValidator()
        {
            RuleFor(p => p.Imagem)
                .NotEmpty().WithMessage("O campo Imagem não pode estar vazio");
        }
    }
}
