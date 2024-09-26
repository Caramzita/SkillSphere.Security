using MediatR;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.Security.Contracts;

namespace SkillSphere.Security.UseCases.Commands.ChangePassword;

/// <summary>
/// Команда для изменения пароля пользователя.
/// </summary>
public class ChangePasswordCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Текущий пароль пользователя.
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// Новый пароль.
    /// </summary>
    public string NewPassword { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ChangePasswordCommand"/>.
    /// </summary>
    /// <param name="login"> Логин, чей пароль нужно изменить. </param>
    /// <param name="password"> Текущий пароль пользователя. </param>
    /// <param name="newPassword"> Новый пароль, на который нужно изменить текущий. </param>
    public ChangePasswordCommand(string login, string password, string newPassword)
    {
        Login = login;
        Password = password;
        NewPassword = newPassword;
    }
}
