using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Invitation
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid InvitedByUserId { get; set; }
    public User InvitedByUser { get; set; } = null!;
    public Guid InvitedContractorUserId { get; set; }
    public User InvitedContractorUser { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
    public InvitationStatus Status { get; set; } = InvitationStatus.Pendiente;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RespondedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}