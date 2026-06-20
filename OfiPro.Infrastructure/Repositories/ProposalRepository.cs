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

    public async Task<List<Proposal>> GetByContractorAsync(Guid contractorUserId)
    {
        return await _context.Proposals
            .Include(x => x.ContractorUser)
            .Include(x => x.ProjectRequirement)
                .ThenInclude(x => x.Project)
            .Where(x => x.ContractorUserId == contractorUserId)
            .ToListAsync();
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

    public async Task<Proposal?> GetByRequirementAndContractorAsync(
        Guid projectRequirementId,
        Guid contractorUserId)
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
}