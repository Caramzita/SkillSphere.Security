using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using SkillSphere.Security.Core;

namespace SkillSphere.Security.DataAccess;

/// <summary>
/// Контекст базы данных для работы с пользовательскими данными и токенами обновления.
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<DatabaseContext> _logger;

    /// <summary>
    /// Таблица пользователей.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Таблица токенов обновления.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DatabaseContext"/>.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <param name="logger">Логгер для записи информации о действиях в контексте базы данных.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger) 
        : base(options)
    {
        _logger = logger;
    }

    /// <summary>
    /// Настраивает сущности модели с использованием конфигураций из сборки.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели, используемый для настройки сущностей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Настраивает параметры подключения к базе данных.
    /// </summary>
    /// <param name="optionsBuilder">Построитель параметров, используемый для настройки подключения.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _logger.LogWarning("DbContextOptionsBuilder is not configured.");
        }

        optionsBuilder.EnableSensitiveDataLogging()
                      .LogTo(log => _logger.LogInformation(log));
    }
}
