using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Contract;

public class ContractDto
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public Guid ProjectRequirementId { get; set; }
    public Guid ClientUserId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public Guid ContractorUserId { get; set; }
    public string ContractorName { get; set; } = string.Empty;
    public string ProjectTitle { get; set; } = string.Empty;
    public string RequirementDescription { get; set; } = string.Empty;
    public decimal AgreedPrice { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public ContractStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
}