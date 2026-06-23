namespace OfiPro.Application.DTOs.Dashboard;

public class ContractorAvailableProjectDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public string Urgency { get; set; } = string.Empty;
    public int RequirementsCount { get; set; }
    public DateTime CreatedAt { get; set; }
}