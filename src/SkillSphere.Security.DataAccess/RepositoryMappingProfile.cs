using AutoMapper;
using SkillSphere.Security.Core;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.DataAccess;

/// <summary>
/// Профиль маппинга для AutoMapper, определяющий преобразования между сущностями домена и DTO.
/// </summary>
public class RepositoryMappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RepositoryMappingProfile"/> и задает конфигурации маппинга.
    /// </summary>
    public RepositoryMappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();

        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
    }
}
