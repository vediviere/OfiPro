namespace OfiPro.Application.DTOs.Dashboard;

public class ClientPendingProposalDto
{
    public Guid ProposalId { get; set; }
    public Guid ProjectRequirementId { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public Guid ContractorUserId { get; set; }
    public string ContractorUserName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}