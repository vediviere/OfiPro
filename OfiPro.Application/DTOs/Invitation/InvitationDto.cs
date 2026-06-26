using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Invitation;

public class InvitationDto
{
    public Guid InvitationId { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public Guid InvitedByUserId { get; set; }
    public string InvitedByUserName { get; set; } = string.Empty;
    public Guid InvitedContractorUserId { get; set; }
    public string InvitedContractorUserName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public InvitationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? RespondedAt { get; set; }
}