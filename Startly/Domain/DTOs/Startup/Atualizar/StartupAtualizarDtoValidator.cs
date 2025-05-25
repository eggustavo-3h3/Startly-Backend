using FluentValidation;

namespace Startly.Domain.DTOs.Startup.Atualizar
{
    public class StartupAtualizarDtoValidator : AbstractValidator<StartupAtualizarDto>
    {
        public StartupAtualizarDtoValidator()
        {
            RuleFor(p => p.Nome)
                .MaximumLength(100).WithMessage("O campo Nome deve ter no minímo 100 caracteres")
                .NotEmpty().WithMessage("O campo Nome não pode estar vazio");

            RuleFor(p => p.Descricao)
                .MaximumLength(2000).WithMessage("O campo Descrição deve ter no minímo 2000 caracteres")
                .NotEmpty().WithMessage("O campo Descrição não pode estar vazio");

            RuleFor(p => p.QuantidadeFuncionario)
                 .NotEmpty().WithMessage("O campo Cep não pode estar vázio");

            RuleFor(p => p.EnumTicket)
                .NotEmpty().WithMessage("O campo de Ticket Médio não pode estar Vázio");

            RuleFor(p => p.EnumTipoDeAtendimento)
                .NotEmpty().WithMessage("O campo Tipo de Atendimento não pode estar vázio");

            RuleFor(p => p.Logo)
                .NotEmpty().WithMessage("O campo Logo não pode estar vazio");
        }
    }
}
