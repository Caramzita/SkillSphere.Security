using MediatR;
using SkillSphere.Security.Contracts;
using System.ComponentModel.DataAnnotations;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.Security.UseCases.Commands.Register;

/// <summary>
/// Команда для регистрации нового пользователя с предоставлением данных для создания учетной записи.
/// </summary>
public class RegisterCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Логин.
    /// </summary>
    [Required]
    public string Login { get; } = string.Empty;

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; } = string.Empty;

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    [Required]
    public string Email { get; } = string.Empty;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RegisterCommand"/>.
    /// </summary>
    /// <param name="login"> Логин. </param>
    /// <param name="password"> Пароль пользователя. </param>
    /// <param name="email"> Адрес электронной почты пользователя. </param>
    public RegisterCommand(string login, string password, string email)
    {
        Login = login;
        Password = password;
        Email = email;
    }
}
