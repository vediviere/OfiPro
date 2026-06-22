using OfiPro.Application.DTOs.Rating;

namespace OfiPro.Application.Interfaces.Services;

public interface IRatingService
{
    Task<RatingDto> CreateAsync(Guid userId, Guid contractId, CreateRatingDto request);
    Task<List<RatingDto>> GetByContractIdAsync(Guid userId, Guid contractId);
    Task<UserReputationDto> GetUserReputationAsync(Guid userId);
    Task<List<RatingDto>> GetReceivedByUserIdAsync(Guid userId);
    Task<List<PublicReceivedRatingDto>> GetPublicReceivedByUserIdAsync(Guid userId);
    Task<PublicUserReputationDto> GetPublicUserReputationAsync(Guid userId);
}