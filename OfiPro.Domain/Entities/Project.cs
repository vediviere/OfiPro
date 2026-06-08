using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Zone { get; set; } = string.Empty;
    public UrgencyLevel Urgency { get; set; } = UrgencyLevel.Flexible;
    public string AvailableMaterials { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.Publicado;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FinishedAt { get; set; }
    public List<ProjectRequirement> Requirements { get; set; } = new();
    public DateTime? DeletedAt { get; set; }
}
