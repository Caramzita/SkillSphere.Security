namespace SkillSphere.Security.Contracts.DTOs;

/// <summary>
/// Представляет DTO для пользователя.
/// </summary>
public class UserEntity
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля пользователя.
    /// </summary>
    public byte[] PasswordHash { get; set; } = [];

    /// <summary>
    /// Соль, использованная для хэширования пароля пользователя.
    /// </summary>
    public byte[] PasswordSalt { get; set; } = [];

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}