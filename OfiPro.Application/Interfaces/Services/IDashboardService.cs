using OfiPro.Application.DTOs.Dashboard;

namespace OfiPro.Application.Interfaces.Services;

public interface IDashboardService
{
    Task<ClientDashboardSummaryDto> GetClientSummaryAsync(Guid userId);
    Task<ContractorDashboardSummaryDto> GetContractorSummaryAsync(Guid userId);
    Task<DashboardModesDto> GetAvailableModesAsync(Guid userId);
    Task<DashboardUserContextDto> GetUserContextAsync(Guid userId);
    Task<AdminDashboardSummaryDto> GetAdminSummaryAsync(Guid userId);
}