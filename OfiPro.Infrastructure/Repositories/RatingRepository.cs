using Microsoft.EntityFrameworkCore;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Domain.Entities;
using OfiPro.Infrastructure.Persistence;

namespace OfiPro.Infrastructure.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly ApplicationDbContext _context;

    public RatingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rating rating)
    {
        await _context.Ratings.AddAsync(rating);
    }

    public async Task<bool> ExistsByContractAndDirectionAsync(Guid contractId, Guid raterUserId, Guid ratedUserId)
    {
        return await _context.Ratings.AnyAsync(x =>
            x.ContractId == contractId &&
            x.RaterUserId == raterUserId &&
            x.RatedUserId == ratedUserId &&
            x.DeletedAt == null);
    }

    public async Task<List<Rating>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.Ratings
            .Include(x => x.RaterUser)
            .Include(x => x.RatedUser)
            .Where(x =>
                x.ContractId == contractId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<Rating>> GetByRatedUserIdAsync(Guid ratedUserId)
    {
        return await _context.Ratings
            .Include(x => x.RaterUser)
            .Include(x => x.RatedUser)
            .Where(x =>
                x.RatedUserId == ratedUserId &&
                x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}