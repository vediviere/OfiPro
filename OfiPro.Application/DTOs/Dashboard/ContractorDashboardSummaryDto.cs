namespace OfiPro.Application.DTOs.Dashboard;

public class ContractorDashboardSummaryDto
{
    public int AvailableProjects { get; set; }
    public int SentProposals { get; set; }
    public int PendingProposals { get; set; }
    public int AcceptedProposals { get; set; }
    public int RejectedProposals { get; set; }
    public int ActiveContracts { get; set; }
    public int PendingStartContracts { get; set; }
    public int InProgressContracts { get; set; }
    public int PendingConfirmationContracts { get; set; }
    public int FinishedContracts { get; set; }
    public int UnreadNotifications { get; set; }
    public double AverageScore { get; set; }
    public int TotalRatings { get; set; }
    public bool HasProfessionalProfile { get; set; }
    public Guid? ProfessionalProfileId { get; set; }
    public bool IsProfessionalProfileActive { get; set; }
    public string MainSpecialty { get; set; } = string.Empty;
    public List<DashboardNotificationDto> RecentNotifications { get; set; } = [];
    public List<ContractorDashboardContractDto> RecentContracts { get; set; } = [];
    public List<ContractorDashboardProposalDto> RecentProposals { get; set; } = [];
    public List<ContractorAvailableProjectDto> AvailableProjectsPreview { get; set; } = [];
}