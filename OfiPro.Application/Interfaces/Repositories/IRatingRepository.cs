using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IRatingRepository
{
    Task AddAsync(Rating rating);
    Task<bool> ExistsByContractAndDirectionAsync(Guid contractId, Guid raterUserId, Guid ratedUserId);
    Task<List<Rating>> GetByContractIdAsync(Guid contractId);
    Task<List<Rating>> GetByRatedUserIdAsync(Guid ratedUserId);
    Task<Dictionary<Guid, (double AverageScore, int TotalRatings)>> GetReputationStatsByUserIdsAsync(
    List<Guid> userIds);
    Task SaveChangesAsync();
}