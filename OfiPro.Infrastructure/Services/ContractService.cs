using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Contract;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<List<ContractDto>> GetMyContractsAsync(Guid userId)
    {
        var contracts = await _contractRepository.GetByUserIdAsync(userId);

        return contracts.Select(MapToDto).ToList();
    }

    public async Task<ContractDto?> GetByIdAsync(Guid contractId, Guid userId)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        if (contract.ClientUserId != userId && contract.ContractorUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para consultar esta contratación.");
        }

        return MapToDto(contract);
    }

    public async Task UpdateStatusAsync(Guid contractId, Guid userId, UpdateContractStatusDto request)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        ValidateUserCanChangeStatus(contract, userId, request.Status);

        ApplyStatusChange(contract, request.Status);

        await _contractRepository.SaveChangesAsync();
    }

    private static void ValidateUserCanChangeStatus(Contract contract, Guid userId, ContractStatus newStatus)
    {
        if (contract.Status == ContractStatus.Finalizado)
        {
            throw new BadRequestException("No se puede cambiar el estado de una contratación finalizada.");
        }

        if (contract.Status == ContractStatus.Cancelado)
        {
            throw new BadRequestException("No se puede cambiar el estado de una contratación cancelada.");
        }

        if (newStatus == ContractStatus.EnProceso)
        {
            if (contract.ContractorUserId != userId)
            {
                throw new ForbiddenException("Solo el contratista puede iniciar la contratación.");
            }

            if (contract.Status != ContractStatus.PendienteInicio)
            {
                throw new BadRequestException("Solo se pueden iniciar contrataciones pendientes de inicio.");
            }

            return;
        }

        if (newStatus == ContractStatus.PendienteConfirmacion)
        {
            if (contract.ContractorUserId != userId)
            {
                throw new ForbiddenException("Solo el contratista puede marcar la contratación como pendiente de confirmación.");
            }

            if (contract.Status != ContractStatus.EnProceso)
            {
                throw new BadRequestException("Solo se pueden enviar a confirmación contrataciones en proceso.");
            }

            return;
        }

        if (newStatus == ContractStatus.Finalizado)
        {
            if (contract.ClientUserId != userId)
            {
                throw new ForbiddenException("Solo el cliente puede finalizar la contratación.");
            }

            if (contract.Status != ContractStatus.PendienteConfirmacion)
            {
                throw new BadRequestException("Solo se pueden finalizar contrataciones pendientes de confirmación.");
            }

            return;
        }

        if (newStatus == ContractStatus.Cancelado)
        {
            if (contract.ClientUserId != userId && contract.ContractorUserId != userId)
            {
                throw new ForbiddenException("No tienes permiso para cancelar esta contratación.");
            }

            if (contract.Status == ContractStatus.Finalizado)
            {
                throw new BadRequestException("No se puede cancelar una contratación finalizada.");
            }

            return;
        }

        throw new BadRequestException("Cambio de estado no permitido.");
    }

    private static void ApplyStatusChange(Contract contract, ContractStatus newStatus)
    {
        contract.Status = newStatus;

        if (newStatus == ContractStatus.EnProceso)
        {
            contract.StartedAt = DateTime.UtcNow;
        }

        if (newStatus == ContractStatus.Finalizado)
        {
            contract.FinishedAt = DateTime.UtcNow;
        }

        if (newStatus == ContractStatus.Cancelado)
        {
            contract.CancelledAt = DateTime.UtcNow;
        }
    }

    private static ContractDto MapToDto(Contract contract)
    {
        return new ContractDto
        {
            ContractId = contract.Id,
            ProposalId = contract.ProposalId,
            ProjectRequirementId = contract.ProjectRequirementId,
            ClientUserId = contract.ClientUserId,
            ClientName = $"{contract.ClientUser.Name} {contract.ClientUser.LastName}",
            ContractorUserId = contract.ContractorUserId,
            ContractorName = $"{contract.ContractorUser.Name} {contract.ContractorUser.LastName}",
            ProjectTitle = contract.ProjectRequirement.Project.Title,
            RequirementDescription = contract.ProjectRequirement.Description,
            AgreedPrice = contract.AgreedPrice,
            EstimatedTime = contract.EstimatedTime,
            Status = contract.Status,
            CreatedAt = contract.CreatedAt,
            StartedAt = contract.StartedAt,
            FinishedAt = contract.FinishedAt,
            CancelledAt = contract.CancelledAt
        };
    }
}