using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupImagemAdicionarDtoValidator : AbstractValidator<StartupImagemAdicionarDto>
    {
        public StartupImagemAdicionarDtoValidator()
        {
            RuleFor(p => p.Imagem)
                .NotEmpty().WithMessage("O campo Imagem não pode estar vazio"); 

            RuleFor(p => p.TipoImagem)
                .NotEmpty().WithMessage("O campo TipoImagem não pode estar vazio")
                .IsInEnum().WithMessage("O campo TipoImagem deve ser um valor válido do enum TipoImagem");

        }

    }
}
