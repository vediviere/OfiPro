namespace OfiPro.Application.DTOs.Dashboard;

public class ClientDashboardSummaryDto
{
    public int TotalProjects { get; set; }
    public int OpenProjects { get; set; }
    public int PendingProposalsReceived { get; set; }
    public int ActiveContracts { get; set; }
    public int PendingConfirmationContracts { get; set; }
    public int FinishedContracts { get; set; }
    public int UnreadNotifications { get; set; }
    public List<DashboardNotificationDto> RecentNotifications { get; set; } = [];
    public List<ClientDashboardContractDto> RecentContracts { get; set; } = [];
    public List<ClientPendingProposalDto> PendingProposalsPreview { get; set; } = [];
}