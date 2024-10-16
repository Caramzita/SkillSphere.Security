using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSphere.Security.UseCases.Commands.ChangePassword;
using SkillSphere.Security.UseCases.Commands.Login;
using SkillSphere.Security.UseCases.Commands.RefreshTokens;
using SkillSphere.Security.UseCases.Commands.Register;
using SkillSphere.Security.Contracts;
using SkillSphere.Security.Contracts.Requests;
using SkillSphere.Infrastructure.UseCases;
using SkillSphere.Security.UseCases.Commands.Users;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.API.Controllers;

/// <summary>
/// Предоставляет Rest API для работы с авторизацией пользователей
/// </summary>
[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Медиатр.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="AuthController"/>.
    /// </summary>
    /// <param name="mediator">Интерфейс для отправки команд и запросов через Mediator.</param>
    /// <param name="mapper">Интерфейс для маппинга данных между моделями.</param>
    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Получение данных о пользователе.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    [HttpGet("users/{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var result = await _mediator.Send(new UserCommand(userId));

        return result.ToActionResult();
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request"> Данные пользователя </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    /// <response code="409"> Пользователь с переданным логином уже существует </response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Tokens), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(typeof(List<string>), 409)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <returns> Токен доступа и токен обновления. </returns>
    /// <param name="request"> Данные пользователя </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Изменение пароля пользователя.
    /// </summary>
    /// <param name="request"> JSON объект, содержащий логин/старый пароль/новый пароль </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPatch("changePassword")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var command = _mapper.Map<ChangePasswordCommand>(request);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }

    /// <summary>
    /// Получить новую пару токенов по токену обновления.
    /// </summary>
    /// <param name="request"> Запрос на обновление токенов авторизации. </param>
    /// <response code="200"> Успешно </response>
    /// <response code="400"> Переданные параметры не прошли валидацию </response>
    [HttpPost("refreshTokens")]
    [ProducesResponseType(typeof(Tokens), 200)]
    [ProducesResponseType(typeof(List<string>), 400)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = _mapper.Map<RefreshTokensCommand>(request);
        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}
