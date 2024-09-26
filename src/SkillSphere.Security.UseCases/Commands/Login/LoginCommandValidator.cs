using FluentValidation;

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
            .NotEmpty().WithMessage("Login is required.")
            .MinimumLength(3).WithMessage("Login must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Login must not exceed 20 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Login can only contain letters, numbers, and underscores.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
    }
}
