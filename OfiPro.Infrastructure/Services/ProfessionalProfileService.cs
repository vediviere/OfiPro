using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.DTOs.Common;
using OfiPro.Application.DTOs.ProfessionalProfile;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class ProfessionalProfileService : IProfessionalProfileService
{
    private readonly IProfessionalProfileRepository _professionalProfileRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRatingRepository _ratingRepository;

    public ProfessionalProfileService(IProfessionalProfileRepository professionalProfileRepository, IUserRepository userRepository, IRatingRepository ratingRepository)
    {
        _professionalProfileRepository = professionalProfileRepository;
        _userRepository = userRepository;
        _ratingRepository = ratingRepository;
    }

    public async Task<ProfessionalProfileDto> CreateAsync(Guid userId, CreateProfessionalProfileDto request)
    {
        await ValidateContractorRoleAsync(userId);

        var existingProfile = await _professionalProfileRepository.GetByUserIdAsync(userId);

        if (existingProfile is not null)
        {
            throw new BadRequestException("Ya tienes un perfil profesional registrado.");
        }

        var professionalProfile = new ProfessionalProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MainSpecialty = request.MainSpecialty.Trim(),
            Description = request.Description.Trim(),
            YearsExperience = request.YearsExperience,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _professionalProfileRepository.AddAsync(professionalProfile);
        await _professionalProfileRepository.SaveChangesAsync();

        var createdProfile = await _professionalProfileRepository.GetByUserIdAsync(userId);

        return MapToDto(createdProfile!);
    }

    public async Task<ProfessionalProfileDto> GetMyProfileAsync(Guid userId)
    {
        await ValidateContractorRoleAsync(userId);

        var profile = await _professionalProfileRepository.GetByUserIdAsync(userId);

        if (profile is null)
        {
            throw new NotFoundException("Perfil profesional no encontrado.");
        }

        return MapToDto(profile);
    }

    public async Task<ProfessionalProfileDto> UpdateAsync(Guid userId, UpdateProfessionalProfileDto request)
    {
        await ValidateContractorRoleAsync(userId);

        var profile = await _professionalProfileRepository.GetByUserIdAsync(userId);

        if (profile is null)
        {
            throw new NotFoundException("Perfil profesional no encontrado.");
        }

        profile.MainSpecialty = request.MainSpecialty.Trim();
        profile.Description = request.Description.Trim();
        profile.YearsExperience = request.YearsExperience;
        profile.IsActive = request.IsActive;

        await _professionalProfileRepository.SaveChangesAsync();

        return MapToDto(profile);
    }

    public async Task<PaginatedResponseDto<ContractorSearchDto>> SearchContractorsAsync(ContractorSearchQueryDto request)
    {
        var profiles = await _professionalProfileRepository.SearchContractorsAsync(
                request.Specialty,
                request.State,
                request.City,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.SortDirection);

        var totalItems = await _professionalProfileRepository.CountContractorsAsync(request.Specialty, request.State, request.City);

        if (profiles.Count == 0)
        {
            return new PaginatedResponseDto<ContractorSearchDto>(
                new List<ContractorSearchDto>(),
                request.PageNumber,
                request.PageSize,
                totalItems);
        }

        var userIds = profiles
            .Select(profile => profile.UserId)
            .ToList();

        var reputationStats = await _ratingRepository.GetReputationStatsByUserIdsAsync(userIds);

        var result = new List<ContractorSearchDto>();

        foreach (var profile in profiles)
        {
            reputationStats.TryGetValue(profile.UserId, out var stats);

            result.Add(new ContractorSearchDto
            {
                UserId = profile.UserId,
                ProfessionalProfileId = profile.Id,
                UserName = $"{profile.User.Name} {profile.User.LastName}".Trim(),
                State = profile.User.State,
                City = profile.User.City,
                MainSpecialty = profile.MainSpecialty,
                Description = profile.Description,
                YearsExperience = profile.YearsExperience,
                AverageScore = stats.AverageScore,
                TotalRatings = stats.TotalRatings
            });
        }

        return new PaginatedResponseDto<ContractorSearchDto>(result, request.PageNumber, request.PageSize, totalItems);
    }

    public async Task<ContractorSearchDto> GetPublicContractorProfileAsync(Guid userId)
    {
        var isContractor = await _userRepository.HasRoleAsync(userId, UserRoleType.Contratista);

        if (!isContractor)
        {
            throw new NotFoundException("Perfil profesional no encontrado.");
        }

        var profile = await _professionalProfileRepository.GetByUserIdAsync(userId);

        if (profile is null || !profile.IsActive || profile.User.DeletedAt != null || !profile.User.IsActive)
        {
            throw new NotFoundException("Perfil profesional no encontrado.");
        }

        var ratings = await _ratingRepository.GetByRatedUserIdAsync(profile.UserId);

        var totalRatings = ratings.Count;

        var averageScore = totalRatings == 0
            ? 0
            : Math.Round(ratings.Average(x => x.Score), 2);

        return new ContractorSearchDto
        {
            UserId = profile.UserId,
            ProfessionalProfileId = profile.Id,
            UserName = $"{profile.User.Name} {profile.User.LastName}".Trim(),
            State = profile.User.State,
            City = profile.User.City,
            MainSpecialty = profile.MainSpecialty,
            Description = profile.Description,
            YearsExperience = profile.YearsExperience,
            AverageScore = averageScore,
            TotalRatings = totalRatings
        };
    }

    private async Task ValidateContractorRoleAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.DeletedAt != null)
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        var isContractor = await _userRepository.HasRoleAsync(
            userId,
            UserRoleType.Contratista);

        if (!isContractor)
        {
            throw new ForbiddenException("Solo usuarios con rol Contratista pueden administrar un perfil profesional.");
        }
    }

    private static ProfessionalProfileDto MapToDto(ProfessionalProfile profile)
    {
        return new ProfessionalProfileDto
        {
            ProfessionalProfileId = profile.Id,
            UserId = profile.UserId,
            UserName = $"{profile.User.Name} {profile.User.LastName}".Trim(),
            Email = profile.User.Email,
            State = profile.User.State,
            City = profile.User.City,
            MainSpecialty = profile.MainSpecialty,
            Description = profile.Description,
            YearsExperience = profile.YearsExperience,
            IsActive = profile.IsActive,
            CreatedAt = profile.CreatedAt
        };
    }
}