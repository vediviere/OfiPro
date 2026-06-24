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

    public async Task<List<ProfessionalProfile>> SearchContractorsAsync(string? specialty, string? state, string? city, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = BuildContractorSearchQuery(specialty, state, city)
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

        query = ApplyContractorSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountContractorsAsync(string? specialty, string? state, string? city)
    {
        var query = BuildContractorSearchQuery(specialty, state, city);

        return await query.CountAsync();
    }

    private IQueryable<ProfessionalProfile> BuildContractorSearchQuery(string? specialty, string? state, string? city)
    {
        var query = _context.ProfessionalProfiles
            .Include(x => x.User)
            .ThenInclude(x => x.UserRoles)
            .Where(x =>
                x.DeletedAt == null &&
                x.IsActive &&
                x.User.DeletedAt == null &&
                x.User.IsActive &&
                x.User.UserRoles.Any(r => r.Role == UserRoleType.Contratista));

        if (!string.IsNullOrWhiteSpace(specialty))
        {
            query = query.Where(x => x.MainSpecialty.Contains(specialty));
        }

        if (!string.IsNullOrWhiteSpace(state))
        {
            query = query.Where(x => x.User.State.Contains(state));
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            query = query.Where(x => x.User.City.Contains(city));
        }

        return query;
    }

    private static IQueryable<ProfessionalProfile> ApplyContractorSorting(IQueryable<ProfessionalProfile> query, string sortBy, string sortDirection)
    {
        var descending = string.Equals(
            sortDirection,
            "desc",
            StringComparison.OrdinalIgnoreCase);

        return sortBy.Trim().ToLowerInvariant() switch
        {
            "specialty" => descending
                ? query.OrderByDescending(x => x.MainSpecialty)
                : query.OrderBy(x => x.MainSpecialty),

            "city" => descending
                ? query.OrderByDescending(x => x.User.City)
                : query.OrderBy(x => x.User.City),

            "state" => descending
                ? query.OrderByDescending(x => x.User.State)
                : query.OrderBy(x => x.User.State),

            "experience" => descending
                ? query.OrderByDescending(x => x.YearsExperience)
                : query.OrderBy(x => x.YearsExperience),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
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