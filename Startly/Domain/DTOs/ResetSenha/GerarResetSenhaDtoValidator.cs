﻿using FluentValidation;

namespace Startly.Domain.DTOs.ResetSenha;

public class GerarResetSenhaDtoValidator : AbstractValidator<GerarResetSenhaDto>
{
    public GerarResetSenhaDtoValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress().WithMessage("Formato do e-mail inválido")
            .NotEmpty().WithMessage("O campo Email não pode estar vazio");
    }
}