using MediatR;
using SkillSphere.Security.Contracts;
using System.ComponentModel.DataAnnotations;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.Security.UseCases.Commands.Login;

/// <summary>
/// Команда для обработки запроса на вход пользователя в систему.
/// </summary>
public class LoginCommand : IRequest<Result<Tokens>>
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
    /// Инициализирует новый экземпляр <see cref="LoginCommand"/>.
    /// </summary>
    /// <param name="login"> Логин. </param>
    /// <param name="password"> Пароль пользователя для входа. </param>
    public LoginCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
