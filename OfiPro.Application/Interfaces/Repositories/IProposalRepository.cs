using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IProposalRepository
{
    Task<Proposal?> GetByIdAsync(Guid id);
    Task<List<Proposal>> GetByContractorAsync(Guid contractorUserId, int pageNumber, int pageSize, string sortBy,
    string sortDirection);
    Task<int> CountByContractorAsync(Guid contractorUserId);
    Task<List<Proposal>> GetByProjectRequirementAsync(Guid projectRequirementId);
    Task<Proposal?> GetByRequirementAndContractorAsync(Guid projectRequirementId, Guid contractorUserId);
    Task AddAsync(Proposal proposal);
    Task SaveChangesAsync();
    Task<Guid?> GetProjectRequirementOwnerUserIdAsync(Guid projectRequirementId);
}
