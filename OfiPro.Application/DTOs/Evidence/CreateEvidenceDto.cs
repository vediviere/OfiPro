using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Evidence;

public class CreateEvidenceDto
{
    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string FileUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FileType { get; set; } = string.Empty;
}