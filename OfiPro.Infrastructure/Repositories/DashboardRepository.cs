using Microsoft.EntityFrameworkCore;
using OfiPro.Application.DTOs.Dashboard;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Enums;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClientDashboardSummaryDto> GetClientSummaryAsync(Guid userId)
    {
        var totalProjects = await _context.Projects
            .CountAsync(x =>
                x.CreatedByUserId == userId &&
                x.DeletedAt == null);

        var openProjects = await _context.Projects
            .CountAsync(x =>
                x.CreatedByUserId == userId &&
                x.DeletedAt == null &&
                x.Status != ProjectStatus.Finalizado &&
                x.Status != ProjectStatus.Cancelado &&
                x.Status != ProjectStatus.Expirado);

        var pendingProposalsReceived = await _context.Proposals
            .CountAsync(x =>
                x.ProjectRequirement.Project.CreatedByUserId == userId &&
                x.ProjectRequirement.Project.DeletedAt == null &&
                x.Status == ProposalStatus.Pendiente);

        var activeContracts = await _context.Contracts
            .CountAsync(x =>
                x.ClientUserId == userId &&
                x.DeletedAt == null &&
                (
                    x.Status == ContractStatus.PendienteInicio ||
                    x.Status == ContractStatus.EnProceso ||
                    x.Status == ContractStatus.PendienteConfirmacion
                ));

        var pendingConfirmationContracts = await _context.Contracts
            .CountAsync(x =>
                x.ClientUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.PendienteConfirmacion);

        var finishedContracts = await _context.Contracts
            .CountAsync(x =>
                x.ClientUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.Finalizado);

        var unreadNotifications = await _context.Notifications
            .CountAsync(x =>
                x.UserId == userId &&
                x.IsRead == false &&
                x.DeletedAt == null);

        var pendingProposalEntities = await _context.Proposals
            .Include(x => x.ContractorUser)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Where(x =>
                x.ProjectRequirement.Project.CreatedByUserId == userId &&
                x.ProjectRequirement.Project.DeletedAt == null &&
                x.Status == ProposalStatus.Pendiente)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        var pendingProposalsPreview = pendingProposalEntities
            .Select(x => new ClientPendingProposalDto
            {
                ProposalId = x.Id,
                ProjectRequirementId = x.ProjectRequirementId,
                ProjectId = x.ProjectRequirement.ProjectId,
                ProjectTitle = x.ProjectRequirement.Project.Title,
                ContractorUserId = x.ContractorUserId,
                ContractorUserName = x.ContractorUser is null
                    ? string.Empty
                    : $"{x.ContractorUser.Name} {x.ContractorUser.LastName}".Trim(),
                Price = x.Price,
                EstimatedTime = x.EstimatedTime,
                CreatedAt = x.CreatedAt
            })
            .ToList();

        var recentNotifications = await _context.Notifications
            .Where(x =>
                x.UserId == userId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new DashboardNotificationDto
            {
                NotificationId = x.Id,
                Type = x.Type.ToString(),
                Title = x.Title,
                Message = x.Message,
                IsRead = x.IsRead,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        var recentContractEntities = await _context.Contracts
            .Include(x => x.ContractorUser)
            .Where(x =>
                x.ClientUserId == userId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        var recentContracts = recentContractEntities
            .Select(x => new ClientDashboardContractDto
            {
                ContractId = x.Id,
                ContractorUserId = x.ContractorUserId,
                ContractorUserName = x.ContractorUser is null
                    ? string.Empty
                    : $"{x.ContractorUser.Name} {x.ContractorUser.LastName}".Trim(),
                Status = x.Status.ToString(),
                AgreedPrice = x.AgreedPrice,
                EstimatedTime = x.EstimatedTime,
                CreatedAt = x.CreatedAt,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt
            })
            .ToList();

        return new ClientDashboardSummaryDto
        {
            TotalProjects = totalProjects,
            OpenProjects = openProjects,
            PendingProposalsReceived = pendingProposalsReceived,
            ActiveContracts = activeContracts,
            PendingConfirmationContracts = pendingConfirmationContracts,
            FinishedContracts = finishedContracts,
            UnreadNotifications = unreadNotifications,
            RecentNotifications = recentNotifications,
            RecentContracts = recentContracts,
            PendingProposalsPreview = pendingProposalsPreview
        };
    }

    public async Task<ContractorDashboardSummaryDto> GetContractorSummaryAsync(Guid userId)
    {
        var availableProjects = await _context.Projects
            .CountAsync(x =>
                x.CreatedByUserId != userId &&
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado &&
                x.Requirements.Any(r =>
                    !r.Proposals.Any(p =>
                        p.ContractorUserId == userId &&
                        p.Status != ProposalStatus.Retirada)));

        var sentProposals = await _context.Proposals
            .CountAsync(x =>
                x.ContractorUserId == userId);

        var pendingProposals = await _context.Proposals
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.Status == ProposalStatus.Pendiente);

        var acceptedProposals = await _context.Proposals
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.Status == ProposalStatus.Aceptada);

        var rejectedProposals = await _context.Proposals
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.Status == ProposalStatus.Rechazada);

        var activeContracts = await _context.Contracts
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null &&
                (
                    x.Status == ContractStatus.PendienteInicio ||
                    x.Status == ContractStatus.EnProceso ||
                    x.Status == ContractStatus.PendienteConfirmacion
                ));

        var pendingStartContracts = await _context.Contracts
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.PendienteInicio);

        var inProgressContracts = await _context.Contracts
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.EnProceso);

        var pendingConfirmationContracts = await _context.Contracts
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.PendienteConfirmacion);

        var finishedContracts = await _context.Contracts
            .CountAsync(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null &&
                x.Status == ContractStatus.Finalizado);

        var unreadNotifications = await _context.Notifications
            .CountAsync(x =>
                x.UserId == userId &&
                x.IsRead == false &&
                x.DeletedAt == null);

        var receivedRatings = await _context.Ratings
            .Where(x =>
                x.RatedUserId == userId &&
                x.DeletedAt == null)
            .ToListAsync();

        var professionalProfile = await _context.ProfessionalProfiles
            .FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.DeletedAt == null);

        var totalRatings = receivedRatings.Count;

        var averageScore = totalRatings == 0
            ? 0
            : Math.Round(receivedRatings.Average(x => x.Score), 2);

        var recentNotifications = await _context.Notifications
            .Where(x =>
                x.UserId == userId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new DashboardNotificationDto
            {
                NotificationId = x.Id,
                Type = x.Type.ToString(),
                Title = x.Title,
                Message = x.Message,
                IsRead = x.IsRead,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        var recentContractEntities = await _context.Contracts
            .Include(x => x.ClientUser)
            .Where(x =>
                x.ContractorUserId == userId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        var recentContracts = recentContractEntities
            .Select(x => new ContractorDashboardContractDto
            {
                ContractId = x.Id,
                ClientUserId = x.ClientUserId,
                ClientUserName = x.ClientUser is null
                    ? string.Empty
                    : $"{x.ClientUser.Name} {x.ClientUser.LastName}".Trim(),
                Status = x.Status.ToString(),
                AgreedPrice = x.AgreedPrice,
                EstimatedTime = x.EstimatedTime,
                CreatedAt = x.CreatedAt,
                StartedAt = x.StartedAt,
                FinishedAt = x.FinishedAt
            })
            .ToList();

        var availableProjectsPreview = await _context.Projects
            .Where(x =>
                x.CreatedByUserId != userId &&
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado &&
                x.Requirements.Any(r =>
                    !r.Proposals.Any(p =>
                        p.ContractorUserId == userId &&
                        p.Status != ProposalStatus.Retirada)))
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .Select(x => new ContractorAvailableProjectDto
            {
                ProjectId = x.Id,
                Title = x.Title,
                Description = x.Description,
                State = x.State,
                City = x.City,
                Zone = x.Zone,
                Urgency = x.Urgency.ToString(),
                RequirementsCount = x.Requirements.Count,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        var recentProposalEntities = await _context.Proposals
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Where(x =>
                x.ContractorUserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Take(5)
            .ToListAsync();

        var recentProposals = recentProposalEntities
            .Select(x => new ContractorDashboardProposalDto
            {
                ProposalId = x.Id,
                ProjectRequirementId = x.ProjectRequirementId,
                ProjectId = x.ProjectRequirement.ProjectId,
                ProjectTitle = x.ProjectRequirement.Project.Title,
                Status = x.Status.ToString(),
                Price = x.Price,
                EstimatedTime = x.EstimatedTime,
                CreatedAt = x.CreatedAt
            })
            .ToList();

        return new ContractorDashboardSummaryDto
        {
            AvailableProjects = availableProjects,
            SentProposals = sentProposals,
            PendingProposals = pendingProposals,
            AcceptedProposals = acceptedProposals,
            RejectedProposals = rejectedProposals,
            ActiveContracts = activeContracts,
            PendingStartContracts = pendingStartContracts,
            InProgressContracts = inProgressContracts,
            PendingConfirmationContracts = pendingConfirmationContracts,
            FinishedContracts = finishedContracts,
            UnreadNotifications = unreadNotifications,
            AverageScore = averageScore,
            TotalRatings = totalRatings,
            HasProfessionalProfile = professionalProfile is not null,
            ProfessionalProfileId = professionalProfile?.Id,
            IsProfessionalProfileActive = professionalProfile?.IsActive ?? false,
            MainSpecialty = professionalProfile?.MainSpecialty ?? string.Empty,
            RecentNotifications = recentNotifications,
            RecentContracts = recentContracts,
            RecentProposals = recentProposals,
            AvailableProjectsPreview = availableProjectsPreview
        };
    }

    public async Task<AdminDashboardSummaryDto> GetAdminSummaryAsync()
    {
        var totalUsers = await _context.Users
            .CountAsync(x => x.DeletedAt == null);

        var totalClients = await _context.UserRoles
            .Where(x =>
                x.Role == UserRoleType.Cliente &&
                x.User.DeletedAt == null)
            .Select(x => x.UserId)
            .Distinct()
            .CountAsync();

        var totalContractors = await _context.UserRoles
            .Where(x =>
                x.Role == UserRoleType.Contratista &&
                x.User.DeletedAt == null)
            .Select(x => x.UserId)
            .Distinct()
            .CountAsync();

        var totalAdmins = await _context.UserRoles
            .Where(x =>
                x.Role == UserRoleType.Administrador &&
                x.User.DeletedAt == null)
            .Select(x => x.UserId)
            .Distinct()
            .CountAsync();

        var totalProjects = await _context.Projects
            .CountAsync(x => x.DeletedAt == null);

        var publishedProjects = await _context.Projects
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Publicado);

        var assignedProjects = await _context.Projects
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Asignado);

        var finishedProjects = await _context.Projects
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ProjectStatus.Finalizado);

        var totalContracts = await _context.Contracts
            .CountAsync(x => x.DeletedAt == null);

        var activeContracts = await _context.Contracts
            .CountAsync(x =>
                x.DeletedAt == null &&
                (
                    x.Status == ContractStatus.PendienteInicio ||
                    x.Status == ContractStatus.EnProceso ||
                    x.Status == ContractStatus.PendienteConfirmacion
                ));

        var finishedContracts = await _context.Contracts
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ContractStatus.Finalizado);

        var cancelledContracts = await _context.Contracts
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.Status == ContractStatus.Cancelado);

        var totalRatings = await _context.Ratings
            .CountAsync(x => x.DeletedAt == null);

        var unreadNotifications = await _context.Notifications
            .CountAsync(x =>
                x.DeletedAt == null &&
                x.IsRead == false);

        return new AdminDashboardSummaryDto
        {
            TotalUsers = totalUsers,
            TotalClients = totalClients,
            TotalContractors = totalContractors,
            TotalAdmins = totalAdmins,
            TotalProjects = totalProjects,
            PublishedProjects = publishedProjects,
            AssignedProjects = assignedProjects,
            FinishedProjects = finishedProjects,
            TotalContracts = totalContracts,
            ActiveContracts = activeContracts,
            FinishedContracts = finishedContracts,
            CancelledContracts = cancelledContracts,
            TotalRatings = totalRatings,
            UnreadNotifications = unreadNotifications
        };
    }
}