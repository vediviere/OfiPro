using OfiPro.Application.DTOs.Project;

namespace OfiPro.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(Guid userId, CreateProjectDto request);
    Task<ProjectDto> GetByIdAsync(Guid id);
    Task<List<ProjectDto>> GetAllAsync();
    Task<List<ProjectDto>> GetMyProjectsAsync(Guid userId);
    Task<ProjectDto> UpdateAsync(Guid userId, Guid projectId, UpdateProjectDto request);
    Task<ProjectDto> UpdateRequirementsAsync(Guid userId, Guid projectId, UpdateProjectRequirementsDto request);
    Task DeleteAsync(Guid userId, Guid projectId);    
}