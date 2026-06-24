using OfiPro.Application.DTOs.Common;
using OfiPro.Application.DTOs.ProfessionalProfile;

namespace OfiPro.Application.Interfaces.Services;

public interface IProfessionalProfileService
{
    Task<ProfessionalProfileDto> CreateAsync(Guid userId, CreateProfessionalProfileDto request);
    Task<ProfessionalProfileDto> GetMyProfileAsync(Guid userId);
    Task<ProfessionalProfileDto> UpdateAsync(Guid userId, UpdateProfessionalProfileDto request);
    Task<PaginatedResponseDto<ContractorSearchDto>> SearchContractorsAsync(ContractorSearchQueryDto request);
    Task<ContractorSearchDto> GetPublicContractorProfileAsync(Guid userId);
}