using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSphere.Security.Core;

namespace SkillSphere.Security.DataAccess.Configurations;

/// <summary>
/// Конфигурирует сущность <see cref="RefreshTokenEntity"/> в Entity Framework Core.
/// </summary>
public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Выполняет настройку конфигурации для сущности <see cref="RefreshTokenEntity"/>.
    /// </summary>
    /// <param name="builder">Строитель конфигурации сущности <see cref="RefreshTokenEntity"/>.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(token => token.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(token => token.UserId);
    }
}
