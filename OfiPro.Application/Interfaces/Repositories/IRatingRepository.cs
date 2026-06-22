using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IRatingRepository
{
    Task AddAsync(Rating rating);
    Task<bool> ExistsByContractAndDirectionAsync(Guid contractId, Guid raterUserId, Guid ratedUserId);
    Task<List<Rating>> GetByContractIdAsync(Guid contractId);
    Task<List<Rating>> GetByRatedUserIdAsync(Guid ratedUserId);
    Task SaveChangesAsync();
}