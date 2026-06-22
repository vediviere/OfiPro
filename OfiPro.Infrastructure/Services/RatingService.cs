using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Rating;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;

    public RatingService(IRatingRepository ratingRepository, IContractRepository contractRepository, IUserRepository userRepository)
    {
        _ratingRepository = ratingRepository;
        _contractRepository = contractRepository;
        _userRepository = userRepository;
    }

    public async Task<RatingDto> CreateAsync(Guid userId, Guid contractId, CreateRatingDto request)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        if (contract.Status != ContractStatus.Finalizado)
        {
            throw new BadRequestException("Solo se pueden calificar contrataciones finalizadas.");
        }

        if (contract.ClientUserId != userId && contract.ContractorUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para calificar esta contratación.");
        }

        var ratedUserId = GetRatedUserId(contract, userId);

        if (ratedUserId == userId)
        {
            throw new BadRequestException("No puedes calificarte a ti mismo.");
        }

        var ratingAlreadyExists = await _ratingRepository.ExistsByContractAndDirectionAsync(
            contractId,
            userId,
            ratedUserId);

        if (ratingAlreadyExists)
        {
            throw new BadRequestException("Ya existe una calificación en esta dirección para esta contratación.");
        }

        var rating = new Rating
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            RaterUserId = userId,
            RatedUserId = ratedUserId,
            Score = request.Score,
            Comment = request.Comment.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _ratingRepository.AddAsync(rating);
        await _ratingRepository.SaveChangesAsync();

        rating.RaterUser = userId == contract.ClientUserId
            ? contract.ClientUser
            : contract.ContractorUser;

        rating.RatedUser = ratedUserId == contract.ClientUserId
            ? contract.ClientUser
            : contract.ContractorUser;

        return MapToDto(rating);
    }

    public async Task<List<RatingDto>> GetByContractIdAsync(Guid userId, Guid contractId)
    {
        var contract = await _contractRepository.GetByIdAsync(contractId);

        if (contract is null)
        {
            throw new NotFoundException("Contratación no encontrada.");
        }

        if (contract.ClientUserId != userId && contract.ContractorUserId != userId)
        {
            throw new ForbiddenException("No tienes permiso para consultar las calificaciones de esta contratación.");
        }

        var ratings = await _ratingRepository.GetByContractIdAsync(contractId);

        return ratings
            .Select(MapToDto)
            .ToList();
    }

    public async Task<UserReputationDto> GetUserReputationAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var ratings = await _ratingRepository.GetByRatedUserIdAsync(userId);

        var totalRatings = ratings.Count;

        var averageScore = totalRatings == 0
            ? 0
            : Math.Round(ratings.Average(x => x.Score), 2);

        var lastRatingAt = ratings
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => (DateTime?)x.CreatedAt)
            .FirstOrDefault();

        return new UserReputationDto
        {
            UserId = user.Id,
            UserName = $"{user.Name} {user.LastName}".Trim(),
            AverageScore = averageScore,
            TotalRatings = totalRatings,
            LastRatingAt = lastRatingAt
        };
    }

    public async Task<List<RatingDto>> GetReceivedByUserIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var ratings = await _ratingRepository.GetByRatedUserIdAsync(userId);

        return ratings
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<PublicReceivedRatingDto>> GetPublicReceivedByUserIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var ratings = await _ratingRepository.GetByRatedUserIdAsync(userId);

        return ratings
            .Select(x => new PublicReceivedRatingDto
            {
                RaterUserName = x.RaterUser is null
                    ? string.Empty
                    : $"{x.RaterUser.Name} {x.RaterUser.LastName}".Trim(),

                Score = x.Score,
                Comment = x.Comment,
                CreatedAt = x.CreatedAt
            })
            .ToList();
    }

    public async Task<PublicUserReputationDto> GetPublicUserReputationAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var ratings = await _ratingRepository.GetByRatedUserIdAsync(userId);

        var totalRatings = ratings.Count;

        var averageScore = totalRatings == 0
            ? 0
            : Math.Round(ratings.Average(x => x.Score), 2);

        var lastRatingAt = ratings
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => (DateTime?)x.CreatedAt)
            .FirstOrDefault();

        return new PublicUserReputationDto
        {
            UserId = user.Id,
            UserName = $"{user.Name} {user.LastName}".Trim(),
            AverageScore = averageScore,
            TotalRatings = totalRatings,
            LastRatingAt = lastRatingAt,
            Ratings = ratings
                .Select(x => new PublicReceivedRatingDto
                {
                    RaterUserName = x.RaterUser is null
                        ? string.Empty
                        : $"{x.RaterUser.Name} {x.RaterUser.LastName}".Trim(),

                    Score = x.Score,
                    Comment = x.Comment,
                    CreatedAt = x.CreatedAt
                })
                .ToList()
        };
    }

    private static Guid GetRatedUserId(Contract contract, Guid raterUserId)
    {
        if (contract.ClientUserId == raterUserId)
        {
            return contract.ContractorUserId;
        }

        return contract.ClientUserId;
    }

    private static RatingDto MapToDto(Rating rating)
    {
        return new RatingDto
        {
            RatingId = rating.Id,
            ContractId = rating.ContractId,

            RaterUserId = rating.RaterUserId,
            RaterUserName = rating.RaterUser is null
                ? string.Empty
                : $"{rating.RaterUser.Name} {rating.RaterUser.LastName}".Trim(),

            RatedUserId = rating.RatedUserId,
            RatedUserName = rating.RatedUser is null
                ? string.Empty
                : $"{rating.RatedUser.Name} {rating.RatedUser.LastName}".Trim(),

            Score = rating.Score,
            Comment = rating.Comment,
            CreatedAt = rating.CreatedAt
        };
    }
}