using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillSphere.Security.Contracts.DTOs;
using SkillSphere.Security.Core;
using SkillSphere.Security.Core.Interfaces;

namespace SkillSphere.Security.DataAccess.Repositories;

/// <summary>
/// Реализация <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository : IUserRepository
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
    /// Инициализирует новый экземпляр класса <see cref="UserRepository"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="mapper">Сервис для маппинга объектов.</param>
    public UserRepository(DatabaseContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async Task<User> GetByIdAsync(Guid id)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<User>(entity);
    }

    /// <inheritdoc/>
    public async Task<User> GetByLoginAsync(string username)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == username)
            .ConfigureAwait(false);

        return _mapper.Map<User>(entity);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email.ToLower() == email.ToLower())
            .ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task CreateAsync(User user)
    {
        var entity = _mapper.Map<UserEntity>(user);

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(User user)
    {
        var entity = _mapper.Map<UserEntity>(user);

        _context.Users.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(User user)
    {
        var entity = _mapper.Map<UserEntity>(user);

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
