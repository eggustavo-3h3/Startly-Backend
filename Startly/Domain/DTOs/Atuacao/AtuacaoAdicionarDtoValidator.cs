﻿using FluentValidation;

namespace Startly.Domain.DTOs.Atuacao
{
    public class AtuacaoAdicionarDtoValidator : AbstractValidator<AtuacaoAdicionarDto>
    {
        public AtuacaoAdicionarDtoValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Descrição deve ser preenchida Obrigatoriamente")
                .MaximumLength(200).WithMessage("Descrição deve ter no máximo ");
        }
    }
}
