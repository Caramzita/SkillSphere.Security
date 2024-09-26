using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.Security.Contracts.DTOs;

namespace SkillSphere.Security.DataAccess.Configurations;

/// <summary>
/// Конфигурирует сущность <see cref="RefreshTokenEntity"/> в Entity Framework Core.
/// </summary>
public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshTokenEntity>
{
    /// <summary>
    /// Выполняет настройку конфигурации для сущности <see cref="RefreshTokenEntity"/>.
    /// </summary>
    /// <param name="builder">Строитель конфигурации сущности <see cref="RefreshTokenEntity"/>.</param>
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(token => token.Id);

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(token => token.UserId);
    }
}
