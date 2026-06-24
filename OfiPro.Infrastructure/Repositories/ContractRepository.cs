using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;

    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contract contract)
    {
        await _context.Contracts.AddAsync(contract);
    }

    public async Task<bool> ExistsByProposalIdAsync(Guid proposalId)
    {
        return await _context.Contracts
            .AnyAsync(x => x.ProposalId == proposalId && x.DeletedAt == null);
    }

    public async Task<List<Contract>> GetByUserIdAsync(Guid userId, int pageNumber, int pageSize, string sortBy, string sortDirection)
    {
        var query = _context.Contracts
            .Include(x => x.Proposal)
            .Include(x => x.ProjectRequirement)
            .ThenInclude(x => x.Project)
            .Include(x => x.ClientUser)
            .Include(x => x.ContractorUser)
            .Where(x =>
                x.DeletedAt == null &&
                (x.ClientUserId == userId || x.ContractorUserId == userId));

        query = ApplyContractSorting(query, sortBy, sortDirection);

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByUserIdAsync(Guid userId)
    {
        return await _context.Contracts
            .CountAsync(x =>
                x.DeletedAt == null &&
                (x.ClientUserId == userId || x.ContractorUserId == userId));
    }

    private static IQueryable<Contract> ApplyContractSorting(
    IQueryable<Contract> query,
    string sortBy,
    string sortDirection)
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

            "agreedprice" => descending
                ? query.OrderByDescending(x => x.AgreedPrice)
                : query.OrderBy(x => x.AgreedPrice),

            "finishedat" => descending
                ? query.OrderByDescending(x => x.FinishedAt)
                : query.OrderBy(x => x.FinishedAt),

            _ => descending
                ? query.OrderByDescending(x => x.CreatedAt)
                : query.OrderBy(x => x.CreatedAt)
        };
    }

    public async Task<Contract?> GetByIdAsync(Guid contractId)
    {
        return await _context.Contracts
            .Include(x => x.Proposal)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Include(x => x.ClientUser)
            .Include(x => x.ContractorUser)
            .FirstOrDefaultAsync(x =>
                x.Id == contractId &&
                x.DeletedAt == null);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}