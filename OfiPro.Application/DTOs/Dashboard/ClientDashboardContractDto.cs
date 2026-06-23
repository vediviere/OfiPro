namespace OfiPro.Application.DTOs.Dashboard;

public class ClientDashboardContractDto
{
    public Guid ContractId { get; set; }
    public Guid ContractorUserId { get; set; }
    public string ContractorUserName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal AgreedPrice { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}