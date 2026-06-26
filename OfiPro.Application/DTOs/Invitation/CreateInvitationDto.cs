using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Invitation;

public class CreateInvitationDto
{
    [Required]
    public Guid ContractorUserId { get; set; }

    [StringLength(500)]
    public string? Message { get; set; }
}