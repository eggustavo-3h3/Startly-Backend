using FluentValidation;
using System.Data;

namespace Startly.Domain.DTOs.Startup.Adicionar
{
    public class StartupAdicionarDtoValidator : AbstractValidator<StartupAdicionarDto>
    {
        public StartupAdicionarDtoValidator()
        {
            RuleFor(p => p.Nome)
                .MaximumLength(100).WithMessage("O campo Nome deve ter no minímo 100 caracteres")
                .NotEmpty().WithMessage("O campo Nome não pode estar vazio");

            RuleFor(p => p.Descricao)
                .MaximumLength(2000).WithMessage("O campo Descrição deve ter no minímo 2000 caracteres")
                .NotEmpty().WithMessage("O campo Descrição não pode estar vazio");

            RuleFor(p => p.Metas)
                .MaximumLength (3000).WithMessage("O campo Metas deve ter no máximo 3000 caracteres")
                .NotEmpty().WithMessage("O campo Metas não pode estar vazio");

            RuleFor(p => p.CNPJ)
            .MaximumLength(14).WithMessage("O campo CNPJ deve ter no máximo 14 caracteres");

            RuleFor(p => p.Cep)
                .NotEmpty().WithMessage("O campo Cep não pode estar vázio")
                .MaximumLength(9).WithMessage("O campo Cep deve ter no Máximo 0 caracteres")
                .Matches("^\\d{5}-\\d{3}$").WithMessage("Cep Inválido");
                
            RuleFor(p => p.Logradouro)
                 .NotEmpty().WithMessage("O campo Logradouro não pode estar vázio")
                 .MaximumLength(100).WithMessage("O campo Logradouro deve ter no Máximo 100 caracteres");

            RuleFor(p => p.Numero)
                .NotEmpty().WithMessage("O campo Numero não pode estar vázio")
                .MaximumLength(9).WithMessage("O campo Numero deve ter no Máximo 5 caracteres");

            RuleFor(p => p.Bairro)
                .NotEmpty().WithMessage("O campo Cep não pode estar vázio")
                .MaximumLength(100).WithMessage("O campo Cep deve ter no Máximo 100 caracteres");

            RuleFor(p => p.Municipio)
                .NotEmpty().WithMessage("O campo Municipio não pode estar vázio")
                .MaximumLength(100).WithMessage("O campo Municipio deve ter no Máximo 100 caracteres");

            RuleFor(p => p.UF)
                .NotEmpty().WithMessage("O campo UF não pode estar vázio")
                .MaximumLength(2).WithMessage("O campo Cep deve ter no Máximo 2 caraxteres");

            RuleFor(p => p.SiteStartup)
                 .MaximumLength(500).WithMessage("O campo Site deve ter no máximo 500 caracteres");

            RuleFor(p => p.QuantidadeFuncionario)
                 .NotEmpty().WithMessage("O campo Cep não pode estar vázio");

            RuleFor(p => p.EnumTicket)
                .NotEmpty().WithMessage("O campo de Ticket Médio não pode estar Vázio");

            RuleFor(p => p.EnumTipoDeAtendimento)
                .NotEmpty().WithMessage("O campo Tipo de Atendimento não pode estar vázio");

            RuleFor(p => p.ResponsavelCadastro)
                .NotEmpty().WithMessage("O campo de responsável pelo cadastro não pode estar vázio")
                .MaximumLength(100).WithMessage("O campo de responsável pelo cadastro deve ter no Máximo 100 caracteres");

            RuleFor(p => p.Senha)
                .NotEmpty().WithMessage("O campo Senha não pode estar vázio");

            RuleFor(p => p.ConfirmarSenha)
                .Equal(p => p.Senha).WithMessage("As senhas não coincidem");

            RuleFor(p => p.Atuacoes)
                .NotEmpty().WithMessage("O campo de Atuação não pode estar vázio");

            RuleFor(p => p.Videos)
                .NotEmpty().WithMessage("O campo de Videos não pode estar vázio");

            RuleFor(p => p.Imagens)
                .NotEmpty().WithMessage("O campo de Imagens não pode estar vázio");

            RuleFor(p => p.Contatos)
                .NotEmpty().WithMessage("O campo de Contatos não pode estar vázio");

        }
    }
}
