using OfiPro.Domain.Enums;

namespace OfiPro.Application.DTOs.Evidence;

public class EvidenceDto
{
    public Guid EvidenceId { get; set; }
    public Guid ContractId { get; set; }
    public Guid UploadedByUserId { get; set; }
    public string UploadedByUserName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public EvidenceType EvidenceType { get; set; }
}