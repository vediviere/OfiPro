using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Contract
{
    public Guid Id { get; set; }

    public Guid ProposalId { get; set; }
    public Proposal Proposal { get; set; } = null!;
    public Guid ProjectRequirementId { get; set; }
    public ProjectRequirement ProjectRequirement { get; set; } = null!;
    public Guid ClientUserId { get; set; }
    public User ClientUser { get; set; } = null!;
    public Guid ContractorUserId { get; set; }
    public User ContractorUser { get; set; } = null!;
    public decimal AgreedPrice { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public ContractStatus Status { get; set; } = ContractStatus.PendienteInicio;
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}