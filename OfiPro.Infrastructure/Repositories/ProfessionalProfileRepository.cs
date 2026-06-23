using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Repositories;

public class ProfessionalProfileRepository : IProfessionalProfileRepository
{
    private readonly ApplicationDbContext _context;

    public ProfessionalProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProfessionalProfile?> GetByUserIdAsync(Guid userId)
    {
        return await _context.ProfessionalProfiles
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.DeletedAt == null);
    }

    public async Task<ProfessionalProfile?> GetByIdAsync(Guid id)
    {
        return await _context.ProfessionalProfiles
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.DeletedAt == null);
    }

    public async Task<List<ProfessionalProfile>> SearchContractorsAsync(string? specialty, string? state, string? city)
    {
        var query = _context.ProfessionalProfiles
            .Include(x => x.User)
            .Where(x =>
                x.DeletedAt == null &&
                x.IsActive &&
                x.User.DeletedAt == null &&
                x.User.IsActive &&
                x.User.UserRoles.Any(r => r.Role == UserRoleType.Contratista))
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(specialty))
        {
            query = query.Where(x =>
                x.MainSpecialty.Contains(specialty.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(state))
        {
            query = query.Where(x =>
                x.User.State.Contains(state.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(x =>
                x.User.City.Contains(city.Trim()));
        }

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(ProfessionalProfile professionalProfile)
    {
        await _context.ProfessionalProfiles.AddAsync(professionalProfile);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}