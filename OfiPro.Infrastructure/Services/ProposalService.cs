using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Proposal;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Infrastructure.Repositories;

namespace OfiPro.Infrastructure.Services;

public class ProposalService : IProposalService
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;

    public ProposalService(IProposalRepository proposalRepository, IContractRepository contractRepository, IUserRepository userRepository)
    {
        _proposalRepository = proposalRepository;
        _contractRepository = contractRepository;
        _userRepository = userRepository;
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

    public async Task<List<ProposalDto>> GetMyProposalsAsync(Guid contractorUserId)
    {
        var proposals =
            await _proposalRepository.GetByContractorAsync(contractorUserId);

        return proposals.Select(MapToDto).ToList();
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

    public async Task<List<ProposalDto>> GetByProjectRequirementAsync(Guid projectRequirementId)
    {
        var proposals =
            await _proposalRepository.GetByProjectRequirementAsync(
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

        foreach (var item in requirementProposals)
        {
            item.Status = item.Id == proposal.Id
                ? ProposalStatus.Aceptada
                : ProposalStatus.Rechazada;
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