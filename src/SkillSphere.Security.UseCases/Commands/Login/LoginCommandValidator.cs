﻿using FluentValidation;

namespace SkillSphere.Security.UseCases.Commands.Login;

/// <summary>
/// Валидатор для команды входа пользователя.
/// </summary>
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="LoginCommandValidator"/>.
    /// </summary>
    public LoginCommandValidator()
    {
        RuleFor(command => command.Login)
            .NotEmpty().WithMessage("Login is required.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
