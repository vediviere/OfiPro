using OfiPro.Application.DTOs.Auth;
using OfiPro.Application.Interfaces;
using OfiPro.Domain.Entities;
using OfiPro.Domain.Enums;
using OfiPro.Application.Common.Exceptions;

namespace OfiPro.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterRequestDto request)
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

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            FullName = $"{user.Name} {user.LastName}",
            Email = user.Email,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(480)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new BadRequestException("Credenciales inválidas.");
        }

        var isValidPassword =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!isValidPassword)
        {
            throw new BadRequestException("Credenciales inválidas.");
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            FullName = $"{user.Name} {user.LastName}",
            Email = user.Email,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(480)
        };
    }
}
