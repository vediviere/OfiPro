using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Evidence;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class EvidenceService : IEvidenceService
{
    private readonly IEvidenceRepository _evidenceRepository;
    private readonly IContractRepository _contractRepository;

    public EvidenceService(
        IEvidenceRepository evidenceRepository,
        IContractRepository contractRepository)
    {
        _evidenceRepository = evidenceRepository;
        _contractRepository = contractRepository;
    }

    public async Task<EvidenceDto> CreateAsync(
        Guid userId,
        Guid contractId,
        CreateEvidenceDto request)
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

        if (contract.Status == ContractStatus.Finalizado ||
            contract.Status == ContractStatus.Cancelado)
        {
            throw new BadRequestException("No se pueden subir evidencias a una contratación finalizada o cancelada.");
        }

        var evidence = new Evidence
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            UploadedByUserId = userId,
            Title = request.Title,
            Description = request.Description,
            FileUrl = request.FileUrl,
            FileType = request.FileType,
            CreatedAt = DateTime.UtcNow
        };

        await _evidenceRepository.AddAsync(evidence);
        await _evidenceRepository.SaveChangesAsync();

        evidence.UploadedByUser = contract.ContractorUser;

        return MapToDto(evidence);
    }

    public async Task<List<EvidenceDto>> GetByContractIdAsync(
        Guid userId,
        Guid contractId)
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

    public async Task DeleteAsync(
        Guid userId,
        Guid evidenceId)
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
            Title = evidence.Title,
            Description = evidence.Description,
            FileUrl = evidence.FileUrl,
            FileType = evidence.FileType,
            CreatedAt = evidence.CreatedAt
        };
    }
}