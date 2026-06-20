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

    public async Task<List<Contract>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Contracts
            .Include(x => x.Proposal)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Include(x => x.ClientUser)
            .Include(x => x.ContractorUser)
            .Where(x =>
                x.DeletedAt == null &&
                (x.ClientUserId == userId || x.ContractorUserId == userId))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
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