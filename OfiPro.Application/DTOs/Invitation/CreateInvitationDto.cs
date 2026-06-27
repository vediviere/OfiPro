using System.ComponentModel.DataAnnotations;
using OfiPro.Application.Common.Validation;

namespace OfiPro.Application.DTOs.Invitation;

public class CreateInvitationDto
{
    [Required]
    public Guid ContractorUserId { get; set; }

    [StringLength(500)]
    [NoHtml]
    public string? Message { get; set; }
}