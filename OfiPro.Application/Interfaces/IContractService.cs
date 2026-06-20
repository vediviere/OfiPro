using OfiPro.Application.DTOs.Contract;

namespace OfiPro.Application.Interfaces.Services;

public interface IContractService
{
    Task<List<ContractDto>> GetMyContractsAsync(Guid userId);
    Task<ContractDto?> GetByIdAsync(Guid contractId, Guid userId);
    Task UpdateStatusAsync(Guid contractId, Guid userId, UpdateContractStatusDto request);
}