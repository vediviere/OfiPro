using OfiPro.Application.DTOs.Proposal;

namespace OfiPro.Application.Interfaces;

public interface IProposalService
{
    Task<ProposalDto> CreateAsync(
        Guid contractorUserId,
        CreateProposalDto request);

    Task<ProposalDto> UpdateAsync(
        Guid contractorUserId,
        Guid proposalId,
        UpdateProposalDto request);

    Task<List<ProposalDto>> GetMyProposalsAsync(
        Guid contractorUserId);

    Task<ProposalDto> GetByIdAsync(
        Guid proposalId);

    Task<List<ProposalDto>> GetByProjectRequirementAsync(
        Guid projectRequirementId);

    Task AcceptAsync(
        Guid proposalId);

    Task RejectAsync(
        Guid proposalId);

    Task WithdrawAsync(
        Guid contractorUserId,
        Guid proposalId);
}