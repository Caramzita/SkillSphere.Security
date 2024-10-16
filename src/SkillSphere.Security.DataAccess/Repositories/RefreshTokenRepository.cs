using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillSphere.Security.Contracts.DTOs;
using SkillSphere.Security.Core;
using SkillSphere.Security.Core.Interfaces;

namespace SkillSphere.Security.DataAccess.Repositories;

/// <summary>
/// Реализация <see cref="IRefreshTokenRepository"/>.
/// </summary>
public class RefreshTokenRepository : IRefreshTokenRepository
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    private readonly DatabaseContext _context;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RefreshTokenRepository"/>.
    /// </summary>
    /// <param name="context"> Контекст базы данных. </param>
    /// <param name="mapper"> Сервис для маппинга объектов. </param>
    public RefreshTokenRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async Task<RefreshToken> GetByTokenAsync(string token)
    {
        var entity = await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Token == token)
            .ConfigureAwait(false);

        return _mapper.Map<RefreshToken>(entity);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(RefreshToken token)
    {
        await _context.RefreshTokens.AddAsync(token).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task DeactivateAsync(RefreshToken token)
    {
        token.Deactivate();

        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task DeactivateAllTokensAsync(Guid userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ToListAsync()
            .ConfigureAwait(false);

        tokens.ForEach(x => x.Deactivate());
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
