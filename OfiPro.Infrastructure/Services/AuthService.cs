using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using OfiPro.Application.Common.Exceptions;
using OfiPro.Application.Common.Settings;
using OfiPro.Application.DTOs.Auth;
using OfiPro.Application.Interfaces.Repositories;
using OfiPro.Application.Interfaces.Services;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;

namespace OfiPro.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IJwtService jwtService, IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new BadRequestException("El correo ya está registrado.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            State = request.State,
            City = request.City,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        user.UserRoles.Add(new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Role = UserRoleType.Cliente
        });

        await _userRepository.AddAsync(user);

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || user.DeletedAt != null || !user.IsActive)
        {
            throw new BadRequestException("Credenciales inválidas.");
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!isValidPassword)
        {
            throw new BadRequestException("Credenciales inválidas.");
        }

        return await CreateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var tokenHash = HashToken(request.RefreshToken);

        var storedRefreshToken = await _refreshTokenRepository
            .GetByTokenHashAsync(tokenHash);

        if (storedRefreshToken is null)
        {
            throw new BadRequestException("Refresh token inválido.");
        }

        if (storedRefreshToken.RevokedAt is not null)
        {
            throw new BadRequestException("Refresh token revocado.");
        }

        if (storedRefreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new BadRequestException("Refresh token expirado.");
        }

        if (storedRefreshToken.User.DeletedAt is not null || !storedRefreshToken.User.IsActive)
        {
            throw new ForbiddenException("Usuario inactivo o eliminado.");
        }

        var newRefreshTokenValue = GenerateRefreshToken();
        var newRefreshTokenHash = HashToken(newRefreshTokenValue);
        var newRefreshTokenExpiresAt = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenExpiresInDays);

        storedRefreshToken.RevokedAt = DateTime.UtcNow;
        storedRefreshToken.ReplacedByTokenHash = newRefreshTokenHash;

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = storedRefreshToken.UserId,
            TokenHash = newRefreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = newRefreshTokenExpiresAt
        });

        await _refreshTokenRepository.SaveChangesAsync();

        var accessToken = _jwtService.GenerateToken(storedRefreshToken.User);

        return new AuthResponseDto
        {
            UserId = storedRefreshToken.User.Id,
            FullName = $"{storedRefreshToken.User.Name} {storedRefreshToken.User.LastName}",
            Email = storedRefreshToken.User.Email,
            Token = accessToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            RefreshToken = newRefreshTokenValue,
            RefreshTokenExpiresAt = newRefreshTokenExpiresAt
        };
    }

    public async Task RevokeRefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var tokenHash = HashToken(request.RefreshToken);

        var storedRefreshToken = await _refreshTokenRepository
            .GetByTokenHashAsync(tokenHash);

        if (storedRefreshToken is null)
        {
            throw new BadRequestException("Refresh token inválido.");
        }

        if (storedRefreshToken.RevokedAt is not null)
        {
            return;
        }

        storedRefreshToken.RevokedAt = DateTime.UtcNow;

        await _refreshTokenRepository.SaveChangesAsync();
    }

    private async Task<AuthResponseDto> CreateAuthResponseAsync(User user)
    {
        var accessToken = _jwtService.GenerateToken(user);

        var refreshTokenValue = GenerateRefreshToken();
        var refreshTokenHash = HashToken(refreshTokenValue);

        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(
            _jwtSettings.RefreshTokenExpiresInDays);

        await _refreshTokenRepository.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = refreshTokenExpiresAt
        });

        await _refreshTokenRepository.SaveChangesAsync();

        return new AuthResponseDto
        {
            UserId = user.Id,
            FullName = $"{user.Name} {user.LastName}",
            Email = user.Email,
            Token = accessToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            RefreshToken = refreshTokenValue,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    private static string HashToken(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToBase64String(hashBytes);
    }
}