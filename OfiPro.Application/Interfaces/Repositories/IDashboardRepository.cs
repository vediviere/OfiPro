using OfiPro.Application.DTOs.Dashboard;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IDashboardRepository
{
    Task<ClientDashboardSummaryDto> GetClientSummaryAsync(Guid userId);
    Task<ContractorDashboardSummaryDto> GetContractorSummaryAsync(Guid userId);
    Task<AdminDashboardSummaryDto> GetAdminSummaryAsync();
}