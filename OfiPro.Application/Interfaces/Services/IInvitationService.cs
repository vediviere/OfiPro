using OfiPro.Application.DTOs.Common.Pagination;
using OfiPro.Application.DTOs.Invitation;

namespace OfiPro.Application.Interfaces.Services;

public interface IInvitationService
{
    Task<InvitationDto> CreateAsync(Guid userId, Guid projectId, CreateInvitationDto request);

    Task<PaginatedResponseDto<InvitationDto>> GetSentAsync(Guid userId, PaginationQueryDto request);

    Task<PaginatedResponseDto<InvitationDto>> GetReceivedAsync(Guid contractorUserId, PaginationQueryDto request);

    Task AcceptAsync(Guid contractorUserId, Guid invitationId);

    Task RejectAsync(Guid contractorUserId, Guid invitationId);

    Task CancelAsync(Guid userId, Guid invitationId);
}