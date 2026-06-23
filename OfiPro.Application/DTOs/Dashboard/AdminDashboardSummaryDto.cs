namespace OfiPro.Application.DTOs.Dashboard;

public class AdminDashboardSummaryDto
{
    public int TotalUsers { get; set; }
    public int TotalClients { get; set; }
    public int TotalContractors { get; set; }
    public int TotalAdmins { get; set; }
    public int TotalProjects { get; set; }
    public int PublishedProjects { get; set; }
    public int AssignedProjects { get; set; }
    public int FinishedProjects { get; set; }
    public int TotalContracts { get; set; }
    public int ActiveContracts { get; set; }
    public int FinishedContracts { get; set; }
    public int CancelledContracts { get; set; }
    public int TotalRatings { get; set; }
    public int UnreadNotifications { get; set; }
}