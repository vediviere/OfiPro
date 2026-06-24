using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetAllAsync(int pageNumber, int pageSize, string sortBy, string sortDirection);
    Task<int> CountAvailableAsync();
    Task<List<Project>> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection);
    Task<int> CountByUserIdAsync(Guid userId);
    Task AddAsync(Project project);
    Task SaveChangesAsync();
    Task UpdateRequirementsAsync(Project project, List<ProjectRequirement> requirements);
    Task<int> ExpirePublishedProjectsAsync(DateTime expirationLimitUtc);
}