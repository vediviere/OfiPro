using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Evidence;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Application.DTOs.Notification;

namespace OfiPro.Infrastructure.Services;

public class EvidenceService : IEvidenceService
{
    private readonly IEvidenceRepository _evidenceRepository;
    private readonly IContractRepository _contractRepository;
    private readonly INotificationService _notificationService;

    public EvidenceService(IEvidenceRepository evidenceRepository, IContractRepository contractRepository, INotificationService notificationService)
    {
        _evidenceRepository = evidenceRepository;
        _contractRepository = contractRepository;
        _notificationService = notificationService;
    }

    public async Task<EvidenceDto> CreateAsync(Guid userId, Guid contractId, CreateEvidenceDto request)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        if (contract.ContractorUserId != userId)
        {
            throw new ForbiddenException("Solo el contratista de esta contratación puede subir evidencias.");
        }

        if (contract.Status == ContractStatus.PendienteInicio)
        {
            throw new BadRequestException("No se pueden agregar evidencias a una contratación que aún no ha iniciado.");
        }

        if (contract.Status == ContractStatus.Finalizado ||
            contract.Status == ContractStatus.Cancelado)
        {
            throw new BadRequestException("No se pueden agregar evidencias a una contratación finalizada o cancelada.");
        }

        if (request.EvidenceType is null)
        {
            throw new BadRequestException("El tipo de evidencia es obligatorio.");
        }

        var evidence = new Evidence
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            UploadedByUserId = userId,
            EvidenceType = request.EvidenceType.Value,
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            FileUrl = request.FileUrl.Trim(),
            FileType = request.FileType.Trim().ToLowerInvariant(),
            CreatedAt = DateTime.UtcNow
        };

        await _evidenceRepository.AddAsync(evidence);
        await _evidenceRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = contract.ClientUserId,
            Type = NotificationType.EvidenceUploaded,
            Title = "Nueva evidencia subida",
            Message = "El contratista subió una evidencia en tu contratación.",
            RelatedEntityType = "Contract",
            RelatedEntityId = contract.Id
        });

        evidence.UploadedByUser = contract.ContractorUser;

        return MapToDto(evidence);
    }

    public async Task<List<EvidenceDto>> GetByContractIdAsync(Guid userId, Guid contractId)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        var userBelongsToContract =
            contract.ClientUserId == userId ||
            contract.ContractorUserId == userId;

        if (!userBelongsToContract)
        {
            throw new ForbiddenException("No tienes permiso para consultar las evidencias de esta contratación.");
        }

        var evidences = await _evidenceRepository.GetByContractIdAsync(contractId);

        return evidences
            .Select(MapToDto)
            .ToList();
    }

    public async Task DeleteAsync(Guid userId, Guid evidenceId)
    {
        var evidence = await _evidenceRepository.GetByIdAsync(evidenceId);

        if (evidence is null)
        {
            throw new NotFoundException("Evidencia no encontrada.");
        }

        if (evidence.UploadedByUserId != userId)
        {
            throw new ForbiddenException("Solo el usuario que subió la evidencia puede eliminarla.");
        }

        if (evidence.Contract.Status == ContractStatus.Finalizado ||
            evidence.Contract.Status == ContractStatus.Cancelado)
        {
            throw new BadRequestException("No se pueden eliminar evidencias de una contratación finalizada o cancelada.");
        }

        evidence.DeletedAt = DateTime.UtcNow;

        await _evidenceRepository.SaveChangesAsync();
    }

    private static EvidenceDto MapToDto(Evidence evidence)
    {
        return new EvidenceDto
        {
            EvidenceId = evidence.Id,
            ContractId = evidence.ContractId,
            UploadedByUserId = evidence.UploadedByUserId,
            UploadedByUserName = evidence.UploadedByUser is null
                ? string.Empty
                : $"{evidence.UploadedByUser.Name} {evidence.UploadedByUser.LastName}".Trim(),
            EvidenceType = evidence.EvidenceType,
            Title = evidence.Title,
            Description = evidence.Description,
            FileUrl = evidence.FileUrl,
            FileType = evidence.FileType,
            CreatedAt = evidence.CreatedAt
        };
    }
}