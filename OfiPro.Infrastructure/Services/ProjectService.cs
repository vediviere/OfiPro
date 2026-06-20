using OfiPro.Application.DTOs.Project;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;

namespace OfiPro.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDto> CreateAsync(Guid userId, CreateProjectDto request)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            CreatedByUserId = userId,
            Title = request.Title,
            Description = request.Description,
            State = request.State,
            City = request.City,
            Zone = request.Zone,
            Urgency = request.Urgency,
            AvailableMaterials = request.AvailableMaterials,
            Status = ProjectStatus.Publicado,
            CreatedAt = DateTime.UtcNow,
            Requirements = request.Requirements.Select(requirement => new ProjectRequirement
            {
                Id = Guid.NewGuid(),
                CategoryId = requirement.CategoryId,
                SubcategoryId = requirement.SubcategoryId,
                Description = requirement.Description
            }).ToList()
        };

        await _projectRepository.AddAsync(project);

        var createdProject = await _projectRepository.GetByIdAsync(project.Id);

        if (createdProject is null)
        {
            throw new BadRequestException("No se pudo obtener el proyecto creado.");
        }

        return MapToDto(createdProject);
    }

    public async Task<ProjectDto> GetByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project is null)
        {
            throw new NotFoundException("Proyecto no encontrado.");
        }

        return MapToDto(project);
    }

    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<ProjectDto>> GetMyProjectsAsync(Guid userId)
    {
        var projects = await _projectRepository.GetByUserIdAsync(userId);

        return projects
            .Select(MapToDto)
            .ToList();
    }

    public async Task<ProjectDto> UpdateAsync(Guid userId, Guid projectId, UpdateProjectDto request)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
        {
            throw new NotFoundException("Proyecto no encontrado.");
        }

        if (project.CreatedByUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para modificar este proyecto.");
        }

        if (project.Status != ProjectStatus.Publicado)
        {
            throw new BadRequestException("Solo se pueden modificar proyectos publicados.");
        }

        project.Title = request.Title;
        project.Description = request.Description;
        project.State = request.State;
        project.City = request.City;
        project.Zone = request.Zone;
        project.Urgency = request.Urgency;
        project.AvailableMaterials = request.AvailableMaterials;

        await _projectRepository.SaveChangesAsync();

        var updatedProject = await _projectRepository.GetByIdAsync(project.Id);

        if (updatedProject is null)
        {
            throw new BadRequestException("No se pudo obtener el proyecto actualizado.");
        }

        return MapToDto(updatedProject);
    }

    public async Task<ProjectDto> UpdateRequirementsAsync(Guid userId, Guid projectId, UpdateProjectRequirementsDto request)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
        {
            throw new NotFoundException("Proyecto no encontrado.");
        }

        if (project.CreatedByUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para modificar este proyecto.");
        }

        if (project.Status != ProjectStatus.Publicado)
        {
            throw new BadRequestException("Solo se pueden modificar proyectos publicados.");
        }

        var requirements = request.Requirements
            .Select(requirement => new ProjectRequirement
            {
                Id = Guid.NewGuid(),
                CategoryId = requirement.CategoryId,
                SubcategoryId = requirement.SubcategoryId,
                Description = requirement.Description
            })
            .ToList();

        await _projectRepository.UpdateRequirementsAsync(
            project,
            requirements);

        var updatedProject = await _projectRepository.GetByIdAsync(projectId);

        if (updatedProject is null)
        {
            throw new BadRequestException("No se pudo obtener el proyecto actualizado.");
        }

        return MapToDto(updatedProject);
    }

    public async Task DeleteAsync(Guid userId, Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
        {
            throw new NotFoundException("Proyecto no encontrado.");
        }

        if (project.CreatedByUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para eliminar este proyecto.");
        }

        if (project.Status != ProjectStatus.Publicado)
        {
            throw new BadRequestException("Solo se pueden eliminar proyectos publicados.");
        }

        project.DeletedAt = DateTime.UtcNow;

        await _projectRepository.SaveChangesAsync();
    }

    private static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            ProjectId = project.Id,
            CreatedByUserId = project.CreatedByUserId,
            CreatedByUserName = $"{project.CreatedByUser.Name} {project.CreatedByUser.LastName}",
            Title = project.Title,
            Description = project.Description,
            State = project.State,
            City = project.City,
            Zone = project.Zone,
            Urgency = project.Urgency,
            AvailableMaterials = project.AvailableMaterials,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            Requirements = project.Requirements.Select(requirement => new ProjectRequirementDto
            {
                ProjectRequirementId = requirement.Id,
                CategoryId = requirement.CategoryId,
                CategoryName = requirement.Category.Name,
                SubcategoryId = requirement.SubcategoryId,
                SubcategoryName = requirement.Subcategory.Name,
                Description = requirement.Description
            }).ToList()
        };
    }
}