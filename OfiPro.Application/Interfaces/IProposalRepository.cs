using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces;

public interface IProposalRepository
{
    Task<Proposal?> GetByIdAsync(Guid id);
    Task<List<Proposal>> GetByContractorAsync(Guid contractorUserId);
    Task<List<Proposal>> GetByProjectRequirementAsync(Guid projectRequirementId);
    Task<Proposal?> GetByRequirementAndContractorAsync(Guid projectRequirementId, Guid contractorUserId);
    Task AddAsync(Proposal proposal);
    Task SaveChangesAsync();
}
