using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IContractRepository
{
    Task AddAsync(Contract contract);
    Task<bool> ExistsByProposalIdAsync(Guid proposalId);
    Task<List<Contract>> GetByUserIdAsync(Guid userId);
    Task<Contract?> GetByIdAsync(Guid contractId);
    Task SaveChangesAsync();
}