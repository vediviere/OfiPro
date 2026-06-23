namespace OfiPro.Application.DTOs.Dashboard;

public class DashboardModesDto
{
    public Guid UserId { get; set; }
    public bool CanUseClientMode { get; set; }
    public bool CanUseContractorMode { get; set; }
    public bool CanUseAdminMode { get; set; }
    public List<string> AvailableModes { get; set; } = [];
    public string DefaultMode { get; set; } = string.Empty;
}