using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class EvidenceRepository : IEvidenceRepository
{
    private readonly ApplicationDbContext _context;

    public EvidenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Evidence evidence)
    {
        await _context.Evidences.AddAsync(evidence);
    }

    public async Task<List<Evidence>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.Evidences
            .Include(x => x.UploadedByUser)
            .Include(x => x.Contract)
            .Where(x =>
                x.ContractId == contractId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Evidence?> GetByIdAsync(Guid evidenceId)
    {
        return await _context.Evidences
            .Include(x => x.UploadedByUser)
            .Include(x => x.Contract)
            .FirstOrDefaultAsync(x =>
                x.Id == evidenceId &&
                x.DeletedAt == null);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}