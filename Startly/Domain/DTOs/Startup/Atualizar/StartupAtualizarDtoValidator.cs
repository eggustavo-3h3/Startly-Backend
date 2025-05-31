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

            RuleFor(p => p.EmailPessoal)
                .MaximumLength(200).WithMessage("O campo Email Pessoal deve ter no máximo 200 caracteres");

            RuleFor(p => p.EmailCorporativo)
                .NotEmpty().WithMessage("O campo Email Corporativo não pode estar vázio")
                .MaximumLength(200).WithMessage("O campo Email Corporativo deve ter no máximo 200 caracteres");

            RuleFor(p => p.LinkedIn)
                .MaximumLength(300).WithMessage("O campo de Link do LinkdIn deve ter no máximo 300 caracteres");

            RuleFor(p => p.TelefoneFixo)
                .NotEmpty().WithMessage("O campo Telefone não pode estar vázio")
                 .MaximumLength(14).WithMessage("O campo Email Pessoal deve ter no máximo 14 caracteres Contando - e ()");

            RuleFor(p => p.QuantidadeFuncionario)
                 .NotEmpty().WithMessage("O campo Cep não pode estar vázio");

            RuleFor(p => p.TicketMedio)
                .NotEmpty().WithMessage("O campo de Ticket Médio não pode estar Vázio");

            RuleFor(p => p.TipoAtendimento)
                .NotEmpty().WithMessage("O campo Tipo de Atendimento não pode estar vázio");

            RuleFor(p => p.Logo)
                .NotEmpty().WithMessage("O campo Logo não pode estar vazio");
        }
    }
}
