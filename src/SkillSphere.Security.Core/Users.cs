using System.Security.Claims;
using SkillSphere.Security.Core.Services;

namespace SkillSphere.Security.Core;

/// <summary>
/// Представляет пользователя системы с данными для аутентификации и авторизации.
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля пользователя.
    /// </summary>
    public byte[] PasswordHash { get; private set; } = new byte[32];

    /// <summary>
    /// Соль, использованная для хеширования пароля пользователя.
    /// </summary>
    public byte[] PasswordSalt { get; private set; } = new byte[32];

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="User"/> с указанным именем пользователя, паролем и электронной почтой.
    /// </summary>
    /// <param name="login"> Логин. </param>
    /// <param name="password"> Пароль пользователя. </param>
    /// <param name="email"> Электронная почта пользователя. </param>
    public User(string login, string password, string email)
    {
        Id = Guid.NewGuid();
        Login = login;
        SetPassword(password);
        Email = email;
    }

    /// <summary>
    /// Конструктор для инициализации модели уже существующего пользователя.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <param name="login"> Логин. </param>
    /// <param name="passwordHash"> Хэш пароля. </param>
    /// <param name="passwordSalt"> Соль пароля. </param>
    /// <param name="email"> Адрес электронной почты. </param>
    public User(Guid id, string login, byte[] passwordHash, byte[] passwordSalt, string email)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Email = email;
    }

    /// <summary>
    /// Устанавливает хэш пароля пользователя и соль.
    /// </summary>
    /// <param name="password">Пароль пользователя.</param>
    public void SetPassword(string password)
    {
        (PasswordHash, PasswordSalt) = CryptographyService.CreatePasswordHash(password);
    }

    /// <summary>
    /// Получает массив утверждений (claims) для аутентификации пользователя.
    /// </summary>
    /// <returns>Массив утверждений (claims) пользователя.</returns>
    public Claim[] GetClaims()
    {
        return
        [
            new(ClaimTypes.NameIdentifier, Id.ToString()),
            new(ClaimTypes.Name, Login),
            new(ClaimTypes.Email, Email)
        ];
    }
}
