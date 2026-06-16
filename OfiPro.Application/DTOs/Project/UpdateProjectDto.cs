using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Project;

public class UpdateProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Flexible;
    public string AvailableMaterials { get; set; } = string.Empty;
}