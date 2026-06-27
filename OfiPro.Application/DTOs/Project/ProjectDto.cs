using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Project;

public class ProjectDto
{
    public Guid ProjectId { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public UrgencyLevel Urgency { get; set; }
    public string UrgencyName { get; set; } = string.Empty;
    public string AvailableMaterials { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<ProjectRequirementDto> Requirements { get; set; } = new();
}
