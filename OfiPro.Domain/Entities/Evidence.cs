using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Evidence
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid UploadedByUserId { get; set; }
    public User UploadedByUser { get; set; } = null!;
    public EvidenceType Type { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
