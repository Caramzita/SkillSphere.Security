using MediatR;
using SkillSphere.Security.Contracts;
using SkillSphere.Infrastructure.UseCases;

namespace SkillSphere.Security.UseCases.Commands.RefreshTokens;

/// <summary>
/// Команда для обновления токенов на основе предоставленного токена обновления.
/// </summary>
public class RefreshTokensCommand : IRequest<Result<Tokens>>
{
    /// <summary>
    /// Получает токен обновления, используемый для запроса нового токена доступа и токена обновления.
    /// </summary>
    public string RefreshToken { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RefreshTokensCommand"/>.
    /// </summary>
    /// <param name="refreshToken"> Токен обновления, используемый для обновления токенов. </param>
    public RefreshTokensCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
