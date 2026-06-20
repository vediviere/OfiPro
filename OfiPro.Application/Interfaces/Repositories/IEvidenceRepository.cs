using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IEvidenceRepository
{
    Task AddAsync(Evidence evidence);
    Task<List<Evidence>> GetByContractIdAsync(Guid contractId);
    Task<Evidence?> GetByIdAsync(Guid evidenceId);
    Task SaveChangesAsync();
}