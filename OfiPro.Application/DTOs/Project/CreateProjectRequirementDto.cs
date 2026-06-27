using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Project;

public class CreateProjectRequirementDto
{
    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    public Guid SubcategoryId { get; set; }

    [Required]
    [StringLength(800, MinimumLength = 10)]
    [NoHtml]
    public string Description { get; set; } = string.Empty;
}
