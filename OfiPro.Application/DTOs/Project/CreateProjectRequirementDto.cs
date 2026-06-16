namespace OfiPro.Application.DTOs.Project;

public class CreateProjectRequirementDto
{
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
}
