using OfiPro.Application.DTOs.Project;
using OfiPro.Application.DTOs.Common.Pagination;

namespace OfiPro.Application.Interfaces.Services;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(Guid userId, CreateProjectDto request);
    Task<ProjectDto> GetByIdAsync(Guid id);
    Task<PaginatedResponseDto<ProjectDto>> GetAllAsync(PaginationQueryDto request);
    Task<PaginatedResponseDto<ProjectDto>> GetMyProjectsAsync(Guid userId, PaginationQueryDto request);
    Task<ProjectDto> UpdateAsync(Guid userId, Guid projectId, UpdateProjectDto request);
    Task<ProjectDto> UpdateRequirementsAsync(Guid userId, Guid projectId, UpdateProjectRequirementsDto request);
    Task DeleteAsync(Guid userId, Guid projectId);    
}