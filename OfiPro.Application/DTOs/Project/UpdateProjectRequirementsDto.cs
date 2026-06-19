namespace OfiPro.Application.DTOs.Project;
using System.ComponentModel.DataAnnotations;

public class UpdateProjectRequirementsDto
{
    [Required]
    [MinLength(1)]
    public List<CreateProjectRequirementDto> Requirements { get; set; } = new();
}