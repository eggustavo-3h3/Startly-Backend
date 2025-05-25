using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupImagemAdicionarDtoValidator : AbstractValidator<StartupImagemAdicionarDto>
    {
        public StartupImagemAdicionarDtoValidator()
        {
            RuleFor(p => p.Imagem)
                .NotEmpty().WithMessage("O campo Imagem não pode estar vazio"); 
        }
    }
}
