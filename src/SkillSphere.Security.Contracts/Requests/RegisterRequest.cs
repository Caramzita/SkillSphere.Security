namespace SkillSphere.Security.Contracts.Requests;

/// <summary>
/// Модель запроса для регистрации.
/// </summary>
/// <param name="Login"> Логин. </param>
/// <param name="Password"> Пароль. </param>
/// <param name="Email"> Почта. </param>
public record RegisterRequest(string Login, string Password, string Email);
