using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfiPro.Domain.Entities;

public class ProjectPhoto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public string Url { get; set; } = string.Empty;
    public int Order { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
