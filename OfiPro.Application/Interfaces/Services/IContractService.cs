using OfiPro.Application.DTOs.Common.Pagination;
using OfiPro.Application.DTOs.Contract;

namespace OfiPro.Application.Interfaces.Services;

public interface IContractService
{
    Task<PaginatedResponseDto<ContractDto>> GetMyContractsAsync(Guid userId, PaginationQueryDto request);
    Task<ContractDto?> GetByIdAsync(Guid contractId, Guid userId);
    Task UpdateStatusAsync(Guid contractId, Guid userId, UpdateContractStatusDto request);
}