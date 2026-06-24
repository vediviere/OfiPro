using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Project project);
    Task SaveChangesAsync();
    Task UpdateRequirementsAsync(Project project, List<ProjectRequirement> requirements);
    Task<int> ExpirePublishedProjectsAsync(DateTime expirationLimitUtc);
}