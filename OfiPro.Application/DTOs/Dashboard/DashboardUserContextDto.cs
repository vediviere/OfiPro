namespace OfiPro.Application.DTOs.Dashboard;

public class DashboardUserContextDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DashboardModesDto Modes { get; set; } = new();
}