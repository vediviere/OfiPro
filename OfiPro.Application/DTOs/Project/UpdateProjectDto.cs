using OfiPro.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Project;

public class UpdateProjectDto
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
    
    [StringLength(1000)]
    public string AvailableMaterials { get; set; } = string.Empty;

    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Flexible;
}