using System.ComponentModel.DataAnnotations;
namespace OfiPro.Application.DTOs.Proposal;
using OfiPro.Application.Common.Validation;

public class CreateProposalDto
{
    [Required]
    public Guid ProjectRequirementId { get; set; }

    [Range(1, 9999999)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 3)]
    [NoHtml]
    public string EstimatedTime { get; set; } = string.Empty;

    public bool IncludesMaterials { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 20)]
    [NoHtml]
    public string ScopeDescription { get; set; } = string.Empty;

    [StringLength(1000)]
    [NoHtml]
    public string Includes { get; set; } = string.Empty;

    [StringLength(1000)]
    [NoHtml]
    public string DoesNotInclude { get; set; } = string.Empty;

    public bool HasWarranty { get; set; }

    [StringLength(500)]
    [NoHtml]
    public string WarrantyDescription { get; set; } = string.Empty;

    [StringLength(500)]
    [NoHtml]
    public string Comment { get; set; } = string.Empty;
}
