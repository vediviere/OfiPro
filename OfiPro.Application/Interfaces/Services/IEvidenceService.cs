using OfiPro.Application.DTOs.Evidence;

namespace OfiPro.Application.Interfaces.Services;

public interface IEvidenceService
{
    Task<EvidenceDto> CreateAsync(
        Guid userId,
        Guid contractId,
        CreateEvidenceDto request);

    Task<List<EvidenceDto>> GetByContractIdAsync(
        Guid userId,
        Guid contractId);

    Task DeleteAsync(
        Guid userId,
        Guid evidenceId);
}