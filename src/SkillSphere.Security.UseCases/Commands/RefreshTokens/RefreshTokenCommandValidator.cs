﻿using FluentValidation;

namespace SkillSphere.Security.UseCases.Commands.RefreshTokens;

/// <summary>
/// Валидатор для команды обновления токена.
/// </summary>
public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RefreshTokenCommandValidator"/>.
    /// </summary>
    public RefreshTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Refresh token cannot be empty.");
    }
}
