using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class ProposalRepository : IProposalRepository
{
    private readonly ApplicationDbContext _context;

    public ProposalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Proposal?> GetByIdAsync(Guid id)
    {
        return await _context.Proposals
            .Include(x => x.ContractorUser)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Proposal>> GetByContractorAsync(Guid contractorUserId, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Proposals
            .Include(x => x.ContractorUser)
            .Include(x => x.ProjectRequirement)
            .ThenInclude(x => x.Project)
            .Where(x => x.ContractorUserId == contractorUserId);

        query = ApplyProposalSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByContractorAsync(Guid contractorUserId)
    {
        return await _context.Proposals
            .CountAsync(x => x.ContractorUserId == contractorUserId);
    }

    private static IQueryable<Proposal> ApplyProposalSorting(IQueryable<Proposal> query, string sortBy, string sortDirection)
    {
        var descending = string.Equals(
            sortDirection,
            "desc",
            StringComparison.OrdinalIgnoreCase);

        return sortBy.Trim().ToLowerInvariant() switch
        {
            "status" => descending
                ? query.OrderByDescending(x => x.Status)
                : query.OrderBy(x => x.Status),

            "price" => descending
                ? query.OrderByDescending(x => x.Price)
                : query.OrderBy(x => x.Price),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
    }

    public async Task<List<Proposal>> GetByProjectRequirementAsync(Guid projectRequirementId)
    {
        return await _context.Proposals
            .Include(x => x.ContractorUser)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Where(x => x.ProjectRequirementId == projectRequirementId)
            .ToListAsync();
    }

    public async Task<Proposal?> GetByRequirementAndContractorAsync(Guid projectRequirementId, Guid contractorUserId)
    {
        return await _context.Proposals
            .FirstOrDefaultAsync(x =>
                x.ProjectRequirementId == projectRequirementId &&
                x.ContractorUserId == contractorUserId);
    }

    public async Task AddAsync(Proposal proposal)
    {
        await _context.Proposals.AddAsync(proposal);

        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Guid?> GetProjectRequirementOwnerUserIdAsync(Guid projectRequirementId)
    {
        return await _context.ProjectRequirements
            .Where(x => x.Id == projectRequirementId)
            .Select(x => (Guid?)x.Project.CreatedByUserId)
            .FirstOrDefaultAsync();
    }
}