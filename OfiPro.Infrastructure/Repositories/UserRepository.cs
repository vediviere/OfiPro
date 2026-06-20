using Microsoft.EntityFrameworkCore;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;
using OfiPro.Domain.Enums;
using OfiPro.Application.Interfaces.Repositories;

namespace OfiPro.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(x => x.UserRoles)
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(x => x.UserRoles)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users
            .AnyAsync(x => x.Email == email);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(x => x.UserRoles)
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<bool> HasRoleAsync(Guid userId, UserRoleType role)
    {
        return await _context.UserRoles
            .AnyAsync(x => x.UserId == userId && x.Role == role);
    }

    public async Task AddRoleAsync(Guid userId, UserRoleType role)
    {
        var alreadyHasRole = await HasRoleAsync(userId, role);

        if (alreadyHasRole)
        {
            return;
        }

        var userRole = new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Role = role
        };

        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }
}
