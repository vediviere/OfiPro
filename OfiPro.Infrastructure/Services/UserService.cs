using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfiPro.Application.DTOs.User;
using OfiPro.Application.Interfaces;

namespace OfiPro.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileDto> GetProfileAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            City = user.City,
            IsActive = user.IsActive
        };
    }

    public async Task<UserProfileDto> UpdateProfileAsync(
        Guid userId,
        UpdateUserProfileDto request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        user.Name = request.Name;
        user.LastName = request.LastName;
        user.Phone = request.Phone;
        user.State = request.State;
        user.City = request.City;

        await _userRepository.UpdateAsync(user);

        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            City = user.City,
            IsActive = user.IsActive
        };
    }
    public async Task<List<UserProfileDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users
            .Where(x => x.DeletedAt == null)
            .Select(user => new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                State = user.State,
                City = user.City,
                IsActive = user.IsActive
            })
            .ToList();
    }

    public async Task<UserProfileDto> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null || user.DeletedAt != null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        return new UserProfileDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            City = user.City,
            IsActive = user.IsActive
        };
    }

    public async Task ActivateAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null || user.DeletedAt != null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        user.IsActive = true;

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeactivateAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null || user.DeletedAt != null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        user.IsActive = false;

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user is null || user.DeletedAt != null)
        {
            throw new Exception("Usuario no encontrado.");
        }

        user.IsActive = false;
        user.DeletedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
    }
}
