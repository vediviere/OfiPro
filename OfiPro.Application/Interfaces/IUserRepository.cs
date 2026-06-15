using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
    Task UpdateAsync(User user);
    Task<List<User>> GetAllAsync();
}