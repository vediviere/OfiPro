using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IProfessionalProfileRepository
{
    Task<ProfessionalProfile?> GetByUserIdAsync(Guid userId);
    Task<ProfessionalProfile?> GetByIdAsync(Guid id);
    Task<List<ProfessionalProfile>> SearchContractorsAsync(string? specialty, string? state, string? city, int pageNumber, int pageSize, string sortBy, string sortDirection);
    Task<int> CountContractorsAsync(string? specialty, string? state, string? city);
    Task AddAsync(ProfessionalProfile professionalProfile);
    Task SaveChangesAsync();
}