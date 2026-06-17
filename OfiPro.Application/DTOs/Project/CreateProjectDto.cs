using System.ComponentModel.DataAnnotations;
using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Project;

public class CreateProjectDto
{
    [Required]
    [StringLength(120, MinimumLength = 5)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 20)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string City { get; set; } = string.Empty;

    [StringLength(120)]
    public string Zone { get; set; } = string.Empty;

    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Flexible;

    [StringLength(1000)]
    public string AvailableMaterials { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "El proyecto debe tener al menos un requerimiento.")]
    public List<CreateProjectRequirementDto> Requirements { get; set; } = new();
}
