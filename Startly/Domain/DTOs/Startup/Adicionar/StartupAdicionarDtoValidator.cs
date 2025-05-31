using FluentValidation;

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

            RuleFor(p => p.Cep)
                .NotEmpty().WithMessage("O campo Cep não pode estar vázio")
                .MaximumLength(9).WithMessage("O campo Cep deve ter no Máximo 9 caracteres")
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
                .MaximumLength(2).WithMessage("O campo UF deve ter no Máximo 2 caracteres");

            RuleFor(p => p.SiteStartup)
                 .MaximumLength(500).WithMessage("O campo Site deve ter no máximo 500 caracteres");

            RuleFor(p => p.QuantidadeFuncionario)
                 .NotEmpty().WithMessage("O campo Quantidade de Funcionarios não pode estar vázio");

            RuleFor(p => p.TicketMedio)
                .NotEmpty().WithMessage("O campo de Ticket Médio não pode estar Vázio");

            RuleFor(p => p.TipoAtendimento)
                .NotEmpty().WithMessage("O campo Tipo de Atendimento não pode estar vázio");

            RuleFor(p => p.ResponsavelCadastro)
                .NotEmpty().WithMessage("O campo de responsável pelo cadastro não pode estar vázio")
                .MaximumLength(100).WithMessage("O campo de responsável pelo cadastro deve ter no Máximo 100 caracteres");

            RuleFor(p => p.Senha)
                .NotEmpty().WithMessage("O campo Senha não pode estar vázio");

            RuleFor(p => p.ConfirmarSenha)
                .Equal(p => p.Senha).WithMessage("As senhas não coincidem");

            RuleFor(p => p.Logo)
                .NotEmpty().WithMessage("O campo Logo não pode estar vazio");

            RuleFor(p => p.Atuacoes)
                .NotEmpty().WithMessage("O campo de Atuação não pode estar vázio");

            RuleFor(p => p.UrlVideo)
                .MaximumLength(300).WithMessage("O campo de Vídeo deve ter no máximo 300 caracteres");

            RuleFor(p => p.Imagens)
                .NotEmpty().WithMessage("O campo de Imagens não pode estar vázio");


        }
    }
}
