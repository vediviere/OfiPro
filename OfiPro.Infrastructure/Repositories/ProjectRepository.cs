using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(x => x.CreatedByUser)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Category)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Subcategory)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.DeletedAt == null);
    }

    public async Task<List<Project>> GetAllAsync(int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Projects
            .Include(x => x.CreatedByUser)
            .Include(x => x.Requirements)
            .ThenInclude(x => x.Category)
            .Include(x => x.Requirements)
            .ThenInclude(x => x.Subcategory)
            .Where(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado);

        query = ApplyProjectSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAvailableAsync()
    {
        return await _context.Projects
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado);
    }

    private static IQueryable<Project> ApplyProjectSorting(IQueryable<Project> query, string sortBy, string sortDirection)
    {
        var descending = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        return sortBy.Trim().ToLowerInvariant() switch
        {
            "title" => descending
                ? query.OrderByDescending(x => x.Title)
                : query.OrderBy(x => x.Title),

            "city" => descending
                ? query.OrderByDescending(x => x.City)
                : query.OrderBy(x => x.City),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
    }

    public async Task<List<Project>> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Projects
            .Include(x => x.CreatedByUser)
            .Include(x => x.Requirements)
            .ThenInclude(x => x.Category)
            .Include(x => x.Requirements)
            .ThenInclude(x => x.Subcategory)
            .Where(x =>
                x.DeletedAt == null &&
                x.CreatedByUserId == userId);

        query = ApplyProjectSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByUserIdAsync(Guid userId)
    {
        return await _context.Projects
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.CreatedByUserId == userId);
    }

    public async Task<int> ExpirePublishedProjectsAsync(DateTime expirationLimitUtc)
    {
        return await _context.Projects
            .Where(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado &&
                x.CreatedAt <= expirationLimitUtc)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(x => x.Status, ProjectStatus.Expirado));
    }

    public async Task<List<Project>> GetPublishedProjectsToExpireAsync(DateTime expirationLimitUtc)
    {
        return await _context.Projects
            .AsNoTracking()
            .Where(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado &&
                x.CreatedAt <= expirationLimitUtc)
            .ToListAsync();
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);

        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRequirementsAsync(Project project, List<ProjectRequirement> requirements)
    {
        var currentRequirements = await _context.ProjectRequirements
            .Where(x => x.ProjectId == project.Id)
            .ToListAsync();

        _context.ProjectRequirements.RemoveRange(currentRequirements);

        foreach (var requirement in requirements)
        {
            requirement.ProjectId = project.Id;
        }

        await _context.ProjectRequirements.AddRangeAsync(requirements);

        await _context.SaveChangesAsync();
    }
}