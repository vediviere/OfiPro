using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Common;
using OfiPro.Application.DTOs.Notification;
using OfiPro.Application.DTOs.Proposal;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class ProposalService : IProposalService
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;

    public ProposalService(IProposalRepository proposalRepository, IContractRepository contractRepository, IUserRepository userRepository, INotificationService notificationService)
    {
        _proposalRepository = proposalRepository;
        _contractRepository = contractRepository;
        _userRepository = userRepository;
        _notificationService = notificationService;
    }

    public async Task<ProposalDto> CreateAsync(Guid contractorUserId, CreateProposalDto request)
    {
        var isContractor = await _userRepository.HasRoleAsync(
        contractorUserId,
        UserRoleType.Contratista);

        if (!isContractor)
        {
            throw new ForbiddenException("Solo los contratistas pueden enviar propuestas.");
        }

        var existingProposal =
            await _proposalRepository.GetByRequirementAndContractorAsync(
                request.ProjectRequirementId,
                contractorUserId);

        if (existingProposal is not null &&
            existingProposal.Status != ProposalStatus.Retirada)
        {
            throw new BadRequestException("Ya tienes una propuesta activa para este requerimiento.");
        }

        var proposal = new Proposal
        {
            Id = Guid.NewGuid(),
            ProjectRequirementId = request.ProjectRequirementId,
            ContractorUserId = contractorUserId,
            Price = request.Price,
            EstimatedTime = request.EstimatedTime,
            IncludesMaterials = request.IncludesMaterials,
            ScopeDescription = request.ScopeDescription,
            Includes = request.Includes,
            DoesNotInclude = request.DoesNotInclude,
            HasWarranty = request.HasWarranty,
            WarrantyDescription = request.WarrantyDescription,
            Comment = request.Comment,
            Status = ProposalStatus.Pendiente,
            CreatedAt = DateTime.UtcNow
        };

        await _proposalRepository.AddAsync(proposal);

        var createdProposal = await _proposalRepository.GetByIdAsync(proposal.Id);

        if (createdProposal is null)
        {
            throw new BadRequestException("No se pudo obtener la propuesta creada.");
        }

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = createdProposal.ProjectRequirement.Project.CreatedByUserId,
            Type = NotificationType.ProposalReceived,
            Title = "Nueva propuesta recibida",
            Message = "Un contratista envió una propuesta para tu proyecto.",
            RelatedEntityType = "Proposal",
            RelatedEntityId = createdProposal.Id
        });

        return MapToDto(createdProposal);
    }

    public async Task<ProposalDto> UpdateAsync(
        Guid contractorUserId,
        Guid proposalId,
        UpdateProposalDto request)
    {
        var proposal = await _proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
        {
            throw new NotFoundException("Propuesta no encontrada.");
        }

        if (proposal.ContractorUserId != contractorUserId)
        {
            throw new ForbiddenException("No tienes permiso para modificar esta propuesta.");
        }

        if (proposal.Status != ProposalStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden modificar propuestas pendientes.");
        }

        proposal.Price = request.Price;
        proposal.EstimatedTime = request.EstimatedTime;
        proposal.IncludesMaterials = request.IncludesMaterials;
        proposal.ScopeDescription = request.ScopeDescription;
        proposal.Includes = request.Includes;
        proposal.DoesNotInclude = request.DoesNotInclude;
        proposal.HasWarranty = request.HasWarranty;
        proposal.WarrantyDescription = request.WarrantyDescription;
        proposal.Comment = request.Comment;

        await _proposalRepository.SaveChangesAsync();

        var updatedProposal = await _proposalRepository.GetByIdAsync(proposal.Id);

        if (updatedProposal is null)
        {
            throw new BadRequestException("No se pudo obtener la propuesta actualizada.");
        }

        return MapToDto(updatedProposal);
    }

    public async Task<PaginatedResponseDto<ProposalDto>> GetMyProposalsAsync(Guid contractorUserId, PaginationQueryDto request)
    {
        var proposals = await _proposalRepository.GetByContractorAsync(
            contractorUserId,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var totalItems = await _proposalRepository.CountByContractorAsync(contractorUserId);

        var proposalDtos = proposals
            .Select(MapToDto)
            .ToList();

        return new PaginatedResponseDto<ProposalDto>(
            proposalDtos,
            request.PageNumber,
            request.PageSize,
            totalItems);
    }

    public async Task<ProposalDto> GetByIdAsync(Guid proposalId)
    {
        var proposal = await _proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
        {
            throw new NotFoundException("Propuesta no encontrada.");
        }

        return MapToDto(proposal);
    }

    public async Task<List<ProposalDto>> GetByProjectRequirementAsync(Guid userId, Guid projectRequirementId)
    {
        var ownerUserId = await _proposalRepository.GetProjectRequirementOwnerUserIdAsync(
            projectRequirementId);

        if (ownerUserId is null)
        {
            throw new NotFoundException("Requerimiento no encontrado.");
        }

        if (ownerUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para consultar las propuestas de este requerimiento.");
        }

        var proposals = await _proposalRepository.GetByProjectRequirementAsync(
            projectRequirementId);

        return proposals.Select(MapToDto).ToList();
    }

    public async Task AcceptAsync(Guid ownerUserId, Guid proposalId)
    {
        var proposal = await _proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
        {
            throw new NotFoundException("Propuesta no encontrada.");
        }

        if (proposal.ProjectRequirement.Project.CreatedByUserId != ownerUserId)
        {
            throw new ForbiddenException("No tienes permiso para aceptar esta propuesta.");
        }

        if (proposal.Status != ProposalStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden aceptar propuestas pendientes.");
        }

        var requirementProposals = await _proposalRepository.GetByProjectRequirementAsync(
            proposal.ProjectRequirementId);

        var rejectedProposals = requirementProposals
            .Where(x => x.Id != proposal.Id && x.Status == ProposalStatus.Pendiente)
            .ToList();

        proposal.Status = ProposalStatus.Aceptada;

        foreach (var rejectedProposal in rejectedProposals)
        {
            rejectedProposal.Status = ProposalStatus.Rechazada;
        }

        var contractExists = await _contractRepository.ExistsByProposalIdAsync(proposal.Id);

        if (!contractExists)
        {
            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                ProposalId = proposal.Id,
                ProjectRequirementId = proposal.ProjectRequirementId,
                ClientUserId = proposal.ProjectRequirement.Project.CreatedByUserId,
                ContractorUserId = proposal.ContractorUserId,
                AgreedPrice = proposal.Price,
                EstimatedTime = proposal.EstimatedTime,
                Status = ContractStatus.PendienteInicio,
                CreatedAt = DateTime.UtcNow
            };

            await _contractRepository.AddAsync(contract);
        }

        await _proposalRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = proposal.ContractorUserId,
            Type = NotificationType.ProposalAccepted,
            Title = "Propuesta aceptada",
            Message = "Tu propuesta fue aceptada por el cliente.",
            RelatedEntityType = "Proposal",
            RelatedEntityId = proposal.Id
        });

        foreach (var rejectedProposal in rejectedProposals)
        {
            await _notificationService.CreateAsync(new CreateNotificationDto
            {
                UserId = rejectedProposal.ContractorUserId,
                Type = NotificationType.ProposalRejected,
                Title = "Propuesta rechazada",
                Message = "Tu propuesta fue rechazada porque el cliente aceptó otra propuesta para este requerimiento.",
                RelatedEntityType = "Proposal",
                RelatedEntityId = rejectedProposal.Id
            });
        }
    }

    public async Task RejectAsync(Guid ownerUserId, Guid proposalId)
    {
        var proposal = await _proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
        {
            throw new NotFoundException("Propuesta no encontrada.");
        }

        if (proposal.ProjectRequirement.Project.CreatedByUserId != ownerUserId)
        {
            throw new ForbiddenException("No tienes permiso para rechazar esta propuesta.");
        }

        if (proposal.Status != ProposalStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden rechazar propuestas pendientes.");
        }

        proposal.Status = ProposalStatus.Rechazada;

        await _proposalRepository.SaveChangesAsync();

        await _notificationService.CreateAsync(new CreateNotificationDto
        {
            UserId = proposal.ContractorUserId,
            Type = NotificationType.ProposalRejected,
            Title = "Propuesta rechazada",
            Message = "Tu propuesta fue rechazada por el cliente.",
            RelatedEntityType = "Proposal",
            RelatedEntityId = proposal.Id
        });
    }

    public async Task WithdrawAsync(
        Guid contractorUserId,
        Guid proposalId)
    {
        var proposal = await _proposalRepository.GetByIdAsync(proposalId);

        if (proposal is null)
        {
            throw new NotFoundException("Propuesta no encontrada.");
        }

        if (proposal.ContractorUserId != contractorUserId)
        {
            throw new ForbiddenException("No tienes permiso para retirar esta propuesta.");
        }

        if (proposal.Status != ProposalStatus.Pendiente)
        {
            throw new BadRequestException("Solo se pueden retirar propuestas pendientes.");
        }

        proposal.Status = ProposalStatus.Retirada;

        await _proposalRepository.SaveChangesAsync();
    }

    private static ProposalDto MapToDto(Proposal proposal)
    {
        return new ProposalDto
        {
            ProposalId = proposal.Id,
            ProjectRequirementId = proposal.ProjectRequirementId,
            ProjectId = proposal.ProjectRequirement.ProjectId,
            ProjectTitle = proposal.ProjectRequirement.Project.Title,
            RequirementDescription = proposal.ProjectRequirement.Description,
            ContractorUserId = proposal.ContractorUserId,
            ContractorName = $"{proposal.ContractorUser.Name} {proposal.ContractorUser.LastName}",
            Price = proposal.Price,
            EstimatedTime = proposal.EstimatedTime,
            IncludesMaterials = proposal.IncludesMaterials,
            ScopeDescription = proposal.ScopeDescription,
            Includes = proposal.Includes,
            DoesNotInclude = proposal.DoesNotInclude,
            HasWarranty = proposal.HasWarranty,
            WarrantyDescription = proposal.WarrantyDescription,
            Comment = proposal.Comment,
            Status = proposal.Status,
            CreatedAt = proposal.CreatedAt
        };
    }

}