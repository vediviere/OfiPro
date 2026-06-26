using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IInvitationRepository
{
    Task<Invitation?> GetByIdAsync(Guid invitationId);

    Task<List<Invitation>> GetSentByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection);

    Task<int> CountSentByUserIdAsync(Guid userId);

    Task<List<Invitation>> GetReceivedByContractorUserIdAsync(Guid contractorUserId, int pageNumber, int pageSize, string sortBy, string sortDirection);

    Task<int> CountReceivedByContractorUserIdAsync(Guid contractorUserId);

    Task<bool> HasPendingInvitationAsync(Guid projectId, Guid contractorUserId);

    Task AddAsync(Invitation invitation);

    Task SaveChangesAsync();
}