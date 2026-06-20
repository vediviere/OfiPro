using OfiPro.Application.DTOs.Proposal;

namespace OfiPro.Application.Interfaces.Services;

public interface IProposalService
{
    Task<ProposalDto> CreateAsync(Guid contractorUserId, CreateProposalDto request);

    Task<ProposalDto> UpdateAsync(Guid contractorUserId, Guid proposalId, UpdateProposalDto request);

    Task<List<ProposalDto>> GetMyProposalsAsync(Guid contractorUserId);

    Task<ProposalDto> GetByIdAsync(Guid proposalId);

    Task<List<ProposalDto>> GetByProjectRequirementAsync(Guid userId, Guid projectRequirementId);

    Task AcceptAsync(Guid ownerUserId, Guid proposalId);

    Task RejectAsync(Guid ownerUserId, Guid proposalId);

    Task WithdrawAsync(Guid contractorUserId, Guid proposalId);
}