using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Evidence
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }
    public Contract Contract { get; set; } = null!;

    public Guid UploadedByUserId { get; set; }
    public User UploadedByUser { get; set; } = null!;
    public EvidenceType EvidenceType { get; set; } = EvidenceType.Antes;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}