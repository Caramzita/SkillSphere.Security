using FluentValidation;

namespace SkillSphere.Security.UseCases.Commands.Register;

/// <summary>
/// Валидатор для команды регистрации пользователя. 
/// Проверяет корректность данных, предоставленных для регистрации.
/// </summary>
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RegisterCommandValidator"/>.
    /// </summary>
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login is required.")
            .MinimumLength(3).WithMessage("Login must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Login must not exceed 20 characters.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Login can only contain letters, numbers, and underscores.");

        RuleFor(command => command.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");

        RuleFor(command => command.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}
