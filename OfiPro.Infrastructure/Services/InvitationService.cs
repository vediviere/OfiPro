using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Common.Pagination;
using OfiPro.Application.DTOs.Invitation;
using OfiPro.Application.DTOs.Notification;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class InvitationService : IInvitationService
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProfessionalProfileRepository _professionalProfileRepository;
    private readonly INotificationService _notificationService;

    public InvitationService(IInvitationRepository invitationRepository, IProjectRepository projectRepository, IUserRepository userRepository,    IProfessionalProfileRepository professionalProfileRepository,
        INotificationService notificationService)
    {
        _invitationRepository = invitationRepository;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _professionalProfileRepository = professionalProfileRepository;
        _notificationService = notificationService;
    }

    public async Task<InvitationDto> CreateAsync(Guid userId, Guid projectId, CreateInvitationDto request)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);

        if (project is null)
        {
            throw new NotFoundException("Proyecto no encontrado.");
        }

        if (project.CreatedByUserId != userId)
        {
            throw new ForbiddenException("Solo el dueño del proyecto puede invitar contratistas.");
        }

        if (project.Status != ProjectStatus.Publicado)
        {
            throw new BadRequestException("Solo se pueden enviar invitaciones para proyectos publicados.");
        }

        if (request.ContractorUserId == userId)
        {
            throw new BadRequestException("No puedes invitarte a tu propio proyecto.");
        }

        var contractor = await _userRepository.GetByIdAsync(request.ContractorUserId);

        if (contractor is null || contractor.DeletedAt != null || !contractor.IsActive)
        {
            throw new NotFoundException("Contratista no encontrado.");
        }

        var isContractor = await _userRepository.HasRoleAsync(request.ContractorUserId, UserRoleType.Contratista);

        if (!isContractor)
        {
            throw new BadRequestException("El usuario invitado no tiene rol Contratista.");
        }

        var professionalProfile = await _professionalProfileRepository
            .GetByUserIdAsync(request.ContractorUserId);

        if (professionalProfile is null || !professionalProfile.IsActive)
        {
            throw new BadRequestException("El contratista no tiene un perfil profesional activo.");
        }

        var hasPendingInvitation = await _invitationRepository
            .HasPendingInvitationAsync(projectId, request.ContractorUserId);

        if (hasPendingInvitation)
        {
            throw new BadRequestException("Ya existe una invitación pendiente para este contratista en este proyecto.");
        }

        var invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            InvitedByUserId = userId,
            InvitedContractorUserId = request.ContractorUserId,
            Message = request.Message?.Trim() ?? string.Empty,
            Status = InvitationStatus.Pendiente,
            CreatedAt = DateTime.UtcNow
        };

        await _invitationRepository.AddAsync(invitation);
        await _invitationRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = request.ContractorUserId,
            Type = NotificationType.ProjectInvitationReceived,
            Title = "Nueva invitación a proyecto",
            Message = $"Te invitaron a revisar el proyecto \"{project.Title}\".",
            RelatedEntityType = "Invitation",
            RelatedEntityId = invitation.Id
        });

        var createdInvitation = await _invitationRepository.GetByIdAsync(invitation.Id);

        return MapToDto(createdInvitation!);
    }

    public async Task<PaginatedResponseDto<InvitationDto>> GetSentAsync(Guid userId, PaginationQueryDto request)
    {
        var invitations = await _invitationRepository.GetSentByUserIdAsync(
            userId,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var totalItems = await _invitationRepository.CountSentByUserIdAsync(userId);

        var invitationDtos = invitations
            .Select(MapToDto)
            .ToList();

        return new PaginatedResponseDto<InvitationDto>(invitationDtos, request.PageNumber, request.PageSize, totalItems);
    }

    public async Task<PaginatedResponseDto<InvitationDto>> GetReceivedAsync(Guid contractorUserId, PaginationQueryDto request)
    {
        var invitations = await _invitationRepository.GetReceivedByContractorUserIdAsync(
            contractorUserId,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var totalItems = await _invitationRepository
            .CountReceivedByContractorUserIdAsync(contractorUserId);

        var invitationDtos = invitations
            .Select(MapToDto)
            .ToList();

        return new PaginatedResponseDto<InvitationDto>(invitationDtos, request.PageNumber, request.PageSize, totalItems);
    }

    public async Task AcceptAsync(Guid contractorUserId, Guid invitationId)
    {
        var invitation = await GetPendingInvitationForContractorAsync(
            contractorUserId,
            invitationId);

        invitation.Status = InvitationStatus.Aceptada;
        invitation.RespondedAt = DateTime.UtcNow;

        await _invitationRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = invitation.InvitedByUserId,
            Type = NotificationType.ProjectInvitationAccepted,
            Title = "Invitación aceptada",
            Message = $"El contratista aceptó revisar tu proyecto \"{invitation.Project.Title}\".",
            RelatedEntityType = "Invitation",
            RelatedEntityId = invitation.Id
        });
    }

    public async Task RejectAsync(Guid contractorUserId, Guid invitationId)
    {
        var invitation = await GetPendingInvitationForContractorAsync(
            contractorUserId,
            invitationId);

        invitation.Status = InvitationStatus.Rechazada;
        invitation.RespondedAt = DateTime.UtcNow;

        await _invitationRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = invitation.InvitedByUserId,
            Type = NotificationType.ProjectInvitationRejected,
            Title = "Invitación rechazada",
            Message = $"El contratista rechazó revisar tu proyecto \"{invitation.Project.Title}\".",
            RelatedEntityType = "Invitation",
            RelatedEntityId = invitation.Id
        });
    }

    public async Task CancelAsync(Guid userId, Guid invitationId)
    {
        var invitation = await _invitationRepository.GetByIdAsync(invitationId);

        if (invitation is null)
        {
            throw new NotFoundException("Invitación no encontrada.");
        }

        if (invitation.InvitedByUserId != userId)
        {
            throw new ForbiddenException("Solo el cliente que envió la invitación puede cancelarla.");
        }

        if (invitation.Status != InvitationStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden cancelar invitaciones pendientes.");
        }

        invitation.Status = InvitationStatus.Cancelada;
        invitation.RespondedAt = DateTime.UtcNow;

        await _invitationRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = invitation.InvitedContractorUserId,
            Type = NotificationType.ProjectInvitationCanceled,
            Title = "Invitación cancelada",
            Message = $"La invitación para revisar el proyecto \"{invitation.Project.Title}\" fue cancelada.",
            RelatedEntityType = "Invitation",
            RelatedEntityId = invitation.Id
        });
    }

    private async Task<Invitation> GetPendingInvitationForContractorAsync(Guid contractorUserId, Guid invitationId)
    {
        var invitation = await _invitationRepository.GetByIdAsync(invitationId);

        if (invitation is null)
        {
            throw new NotFoundException("Invitación no encontrada.");
        }

        if (invitation.InvitedContractorUserId != contractorUserId)
        {
            throw new ForbiddenException("No tienes permiso para responder esta invitación.");
        }

        if (invitation.Status != InvitationStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden responder invitaciones pendientes.");
        }

        return invitation;
    }

    private static InvitationDto MapToDto(Invitation invitation)
    {
        return new InvitationDto
        {
            InvitationId = invitation.Id,
            ProjectId = invitation.ProjectId,
            ProjectTitle = invitation.Project.Title,
            InvitedByUserId = invitation.InvitedByUserId,
            InvitedByUserName = $"{invitation.InvitedByUser.Name} {invitation.InvitedByUser.LastName}".Trim(),
            InvitedContractorUserId = invitation.InvitedContractorUserId,
            InvitedContractorUserName = $"{invitation.InvitedContractorUser.Name} {invitation.InvitedContractorUser.LastName}".Trim(),
            Message = invitation.Message,
            Status = invitation.Status,
            StatusName = invitation.Status.ToString(),
            CreatedAt = invitation.CreatedAt,
            RespondedAt = invitation.RespondedAt
        };
    }
}