using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
    Task UpdateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<bool> HasRoleAsync(Guid userId, UserRoleType role);
    Task AddRoleAsync(Guid userId, UserRoleType role);
}