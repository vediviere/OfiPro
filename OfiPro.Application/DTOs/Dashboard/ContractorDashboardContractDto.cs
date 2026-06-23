namespace OfiPro.Application.DTOs.Dashboard;

public class ContractorDashboardContractDto
{
    public Guid ContractId { get; set; }
    public Guid ClientUserId { get; set; }
    public string ClientUserName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal AgreedPrice { get; set; }
    public string EstimatedTime { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}