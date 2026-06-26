using OfiPro.Application.DTOs.Common.Pagination;

namespace OfiPro.Application.DTOs.ProfessionalProfile;

public class ContractorSearchQueryDto : PaginationQueryDto
{
    public string? Specialty { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }
}