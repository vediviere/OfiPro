using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly ApplicationDbContext _context;

    public InvitationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Invitation?> GetByIdAsync(Guid invitationId)
    {
        return await _context.Invitations
            .Include(x => x.Project)
            .Include(x => x.InvitedByUser)
            .Include(x => x.InvitedContractorUser)
            .FirstOrDefaultAsync(x =>
                x.Id == invitationId &&
                x.DeletedAt == null);
    }

    public async Task<List<Invitation>> GetSentByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Invitations
            .Include(x => x.Project)
            .Include(x => x.InvitedByUser)
            .Include(x => x.InvitedContractorUser)
            .Where(x =>
                x.InvitedByUserId == userId &&
                x.DeletedAt == null);

        query = ApplyInvitationSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountSentByUserIdAsync(Guid userId)
    {
        return await _context.Invitations
            .CountAsync(x =>
                x.InvitedByUserId == userId &&
                x.DeletedAt == null);
    }

    public async Task<List<Invitation>> GetReceivedByContractorUserIdAsync(Guid contractorUserId, int pageNumber, int pageSize, string sortBy,  string sortDirection)
    {
        var query = _context.Invitations
            .Include(x => x.Project)
            .Include(x => x.InvitedByUser)
            .Include(x => x.InvitedContractorUser)
            .Where(x =>
                x.InvitedContractorUserId == contractorUserId &&
                x.DeletedAt == null);

        query = ApplyInvitationSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountReceivedByContractorUserIdAsync(Guid contractorUserId)
    {
        return await _context.Invitations
            .CountAsync(x =>
                x.InvitedContractorUserId == contractorUserId &&
                x.DeletedAt == null);
    }

    public async Task<bool> HasPendingInvitationAsync(Guid projectId, Guid contractorUserId)
    {
        return await _context.Invitations
            .AnyAsync(x =>
                x.ProjectId == projectId &&
                x.InvitedContractorUserId == contractorUserId &&
                x.Status == InvitationStatus.Pendiente &&
                x.DeletedAt == null);
    }

    public async Task AddAsync(Invitation invitation)
    {
        await _context.Invitations.AddAsync(invitation);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    private static IQueryable<Invitation> ApplyInvitationSorting(IQueryable<Invitation> query, string sortBy, string sortDirection)
    {
        var descending = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        return sortBy.Trim().ToLowerInvariant() switch
        {
            "status" => descending
                ? query.OrderByDescending(x => x.Status)
                : query.OrderBy(x => x.Status),

            "projecttitle" => descending
                ? query.OrderByDescending(x => x.Project.Title)
                : query.OrderBy(x => x.Project.Title),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
    }
}