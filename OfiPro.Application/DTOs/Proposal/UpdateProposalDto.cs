using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Proposal;

public class UpdateProposalDto
{
    [Range(1, 9999999)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 3)]
    public string EstimatedTime { get; set; } = string.Empty;

    public bool IncludesMaterials { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 20)]
    public string ScopeDescription { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Includes { get; set; } = string.Empty;

    [StringLength(1000)]
    public string DoesNotInclude { get; set; } = string.Empty;

    public bool HasWarranty { get; set; }

    [StringLength(500)]
    public string WarrantyDescription { get; set; } = string.Empty;

    [StringLength(500)]
    public string Comment { get; set; } = string.Empty;
}