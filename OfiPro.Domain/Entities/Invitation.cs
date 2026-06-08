using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfiPro.Domain.Entities;

public class Invitation
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid InvitedByUserId { get; set; }
    public User InvitedByUser { get; set; } = null!;
    public string InvitedName { get; set; } = string.Empty;
    public string InvitedPhone { get; set; } = string.Empty;
    public string Status { get; set; } = "Pendiente";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
