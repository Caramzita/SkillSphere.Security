using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.DataAccess.Configurations;

/// <summary>
/// Конфигурирует сущность <see cref="UserEntity"/> в Entity Framework Core.
/// </summary>
public class UserConfig : IEntityTypeConfiguration<UserEntity>
{
    /// <summary>
    /// Выполняет настройку конфигурации для сущности <see cref="UserEntity"/>.
    /// </summary>
    /// <param name="builder">Строитель конфигурации сущности <see cref="UserEntity"/>.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasAlternateKey(user => user.Login);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
