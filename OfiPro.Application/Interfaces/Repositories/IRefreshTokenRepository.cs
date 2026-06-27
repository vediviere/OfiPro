using OfiPro.Domain.Entities;

namespace OfiPro.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);

    Task AddAsync(RefreshToken refreshToken);

    Task SaveChangesAsync();
}