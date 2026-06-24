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

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(x => x.CreatedByUser)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Category)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Subcategory)
            .Where(x => x.DeletedAt == null && x.Status == ProjectStatus.Publicado)
            .ToListAsync();
    }

    public async Task<List<Project>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Projects
            .Include(x => x.CreatedByUser)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Category)
            .Include(x => x.Requirements)
                .ThenInclude(x => x.Subcategory)
            .Where(x =>
                x.CreatedByUserId == userId &&
                x.DeletedAt == null)
            .ToListAsync();
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