namespace OfiPro.Application.DTOs.Project;

public class ProjectRequirementDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public Guid SubcategoryId { get; set; }
    public string SubcategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}