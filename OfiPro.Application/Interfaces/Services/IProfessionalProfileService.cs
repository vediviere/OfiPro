using OfiPro.Application.DTOs.ProfessionalProfile;

namespace OfiPro.Application.Interfaces.Services;

public interface IProfessionalProfileService
{
    Task<ProfessionalProfileDto> CreateAsync(Guid userId, CreateProfessionalProfileDto request);
    Task<ProfessionalProfileDto> GetMyProfileAsync(Guid userId);
    Task<ProfessionalProfileDto> UpdateAsync(Guid userId, UpdateProfessionalProfileDto request);
    Task<List<ContractorSearchDto>> SearchContractorsAsync(string? specialty, string? state, string? city);
    Task<ContractorSearchDto> GetPublicContractorProfileAsync(Guid userId);
}