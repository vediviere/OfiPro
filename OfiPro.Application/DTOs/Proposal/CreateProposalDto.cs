namespace OfiPro.Application.DTOs.Proposal;

public class CreateProposalDto
{
    public Guid ProjectRequirementId { get; set; }
    public decimal Price { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public bool IncludesMaterials { get; set; }
    public string ScopeDescription { get; set; } = string.Empty;
    public string Includes { get; set; } = string.Empty;
    public string DoesNotInclude { get; set; } = string.Empty;
    public bool HasWarranty { get; set; }
    public string WarrantyDescription { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}
