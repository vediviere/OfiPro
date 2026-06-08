using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfiPro.Domain.Enums;

namespace OfiPro.Domain.Entities;

public class Proposal
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public Guid ContractorUserId { get; set; }
    public User ContractorUser { get; set; } = null!;
    public decimal Price { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public bool IncludesMaterials { get; set; }
    public string ScopeDescription { get; set; } = string.Empty;
    // Ejemplo: "Esta propuesta cubre albañilería y plomería"

    public string Includes { get; set; } = string.Empty;
    public string DoesNotInclude { get; set; } = string.Empty;
    public bool HasWarranty { get; set; }
    public string WarrantyDescription { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public ProposalStatus Status { get; set; } = ProposalStatus.Pendiente;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
