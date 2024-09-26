namespace SkillSphere.Security.Contracts.Requests;

/// <summary>
/// Модель запроса на вход.
/// </summary>
/// <param name="Login"> Логин. </param>
/// <param name="Password"> Пароль </param>
public record LoginRequest(string Login, string Password);