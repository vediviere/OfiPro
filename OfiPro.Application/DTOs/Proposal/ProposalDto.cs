using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Proposal;

public class ProposalDto
{
    public Guid ProposalId { get; set; }
    public Guid ProjectRequirementId { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public string RequirementDescription { get; set; } = string.Empty;
    public Guid ContractorUserId { get; set; }
    public string ContractorName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public bool IncludesMaterials { get; set; }
    public string ScopeDescription { get; set; } = string.Empty;
    public string Includes { get; set; } = string.Empty;
    public string DoesNotInclude { get; set; } = string.Empty;
    public bool HasWarranty { get; set; }
    public string WarrantyDescription { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public ProposalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
