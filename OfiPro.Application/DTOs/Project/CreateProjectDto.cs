using System.ComponentModel.DataAnnotations;
using OfiPro.Domain.Enums;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Project;

public class CreateProjectDto
{
    [Required]
    [StringLength(120, MinimumLength = 5)]
    [NoHtml]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 20)]
    [NoHtml]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    [NoHtml]
    public string State { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    [NoHtml]
    public string City { get; set; } = string.Empty;

    [StringLength(120)]
    [NoHtml]
    public string Zone { get; set; } = string.Empty;

    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Flexible;

    [StringLength(1000)]
    [NoHtml]
    public string AvailableMaterials { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "El proyecto debe tener al menos un requerimiento.")]
    public List<CreateProjectRequirementDto> Requirements { get; set; } = new();
}
