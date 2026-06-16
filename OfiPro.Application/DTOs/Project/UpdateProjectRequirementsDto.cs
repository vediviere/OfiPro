namespace OfiPro.Application.DTOs.Project;

public class UpdateProjectRequirementsDto
{
    public List<CreateProjectRequirementDto> Requirements { get; set; } = new();
}