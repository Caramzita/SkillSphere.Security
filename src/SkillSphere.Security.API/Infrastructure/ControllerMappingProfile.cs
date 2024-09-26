using AutoMapper;
using SkillSphere.Security.UseCases.Commands.ChangePassword;
using SkillSphere.Security.UseCases.Commands.Login;
using SkillSphere.Security.UseCases.Commands.RefreshTokens;
using SkillSphere.Security.UseCases.Commands.Register;
using SkillSphere.Security.Contracts.Requests;
using SkillSphere.Security.Core;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.API.Infrastructure;

/// <summary>
/// Профиль AutoMapper для маппинга объектов запросов из контроллера на команды.
/// </summary>
public class ControllerMappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ControllerMappingProfile"/> и задает конфигурации маппинга.
    /// </summary>
    public ControllerMappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>()
            .ConstructUsing(request => new RegisterCommand
            (request.Login, request.Password, request.Email));

        CreateMap<LoginRequest, LoginCommand>()
            .ConstructUsing(request => new LoginCommand(request.Login, request.Password));

        CreateMap<ChangePasswordRequest, ChangePasswordCommand>()
            .ConstructUsing(request => new ChangePasswordCommand
            (request.Login, request.Password, request.NewPassword));

        CreateMap<RefreshTokenRequest, RefreshTokensCommand>()
            .ConstructUsing(request => new RefreshTokensCommand(request.RefreshToken));

        CreateMap<User, UserDto>();
    }
}
