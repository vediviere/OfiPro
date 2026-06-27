using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.ProfessionalProfile;

public class CreateProfessionalProfileDto
{
    [Required]
    [StringLength(100)]
    [NoHtml]
    public string MainSpecialty { get; set; } = string.Empty;

    [StringLength(1000)]
    [NoHtml]
    public string Description { get; set; } = string.Empty;

    [Range(0, 80)]
    public int YearsExperience { get; set; }
}