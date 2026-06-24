using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfiPro.Application.DTOs.Project;
using OfiPro.Application.Interfaces.Services;
using System.Security.Claims;
using OfiPro.Application.DTOs.Common;

namespace OfiPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectDto request)
    {
        var userId = GetUserId();

        var project = await _projectService.CreateAsync(userId, request);

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryDto query)
    {
        var projects = await _projectService.GetAllAsync(query);

        return Ok(projects);
    }

    [HttpGet("my-projects")]
    public async Task<IActionResult> GetMyProjects([FromQuery] PaginationQueryDto query)
    {
        var userId = GetUserId();

        var projects = await _projectService.GetMyProjectsAsync(userId, query);

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var project = await _projectService.GetByIdAsync(id);

        return Ok(project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectDto request)
    {
        var userId = GetUserId();

        var project = await _projectService.UpdateAsync(userId, id, request);

        return Ok(project);
    }

    [HttpPut("{id}/requirements")]
    public async Task<IActionResult> UpdateRequirements(Guid id, UpdateProjectRequirementsDto request)
    {
        var userId = GetUserId();

        var project = await _projectService.UpdateRequirementsAsync(
            userId,
            id,
            request);

        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetUserId();

        await _projectService.DeleteAsync(userId, id);

        return Ok(new
        {
            message = "Proyecto eliminado."
        });
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("Token inválido.");
        }

        return userId;
    }
}